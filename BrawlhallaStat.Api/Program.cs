using BrawlhallaReplayReader.DependencyInjection;
using BrawlhallaStat.Api.Authentication;
using BrawlhallaStat.Api.Authentication.Services.Tokens;
using BrawlhallaStat.Api.BrawlhallaEntities;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Api.Middlewares;
using BrawlhallaStat.Api.Replays;
using BrawlhallaStat.Api.Statistics;
using BrawlhallaStat.Api.Users;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Authentication.Dto.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace BrawlhallaStat.Api;

public class Program
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BrawlhallaStatContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(
                    connectionString,
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                )
                .UseLoggerFactory(NullLoggerFactory.Instance);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        });

        services.AddSingleton<TokenConfig>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var tokenConfig = new TokenConfig(configuration);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenConfig.Issuer,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = tokenConfig.Audience,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = tokenConfig.GetSymmetricSecurityAccessKey(),
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("Editor", p => p.RequireRole("Editor"));

        services.AddCors(options =>
        {
            options.AddPolicy(name: "WebClient",
                builder =>
                    builder.WithOrigins(
                            "http://localhost:3000",
                            "https://localhost:3000"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
            );
        });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddControllers();

        services.AddValidatorsFromAssemblyContaining<RegisterModelValidator>();


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddBrawlhallaReplayDeserializer();

        services.AddMemoryCache();
        
        services.AddReplay();
        services.AddStatistic();
        services.AddBrawlhallaEntities();
        services.AddUserService();

        services.AddAuth();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        app.UseMiddleware<DelayMiddleware>();
        app.UseMiddleware<ApiExceptionHandlerMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("WebClient");

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();

        scope.ServiceProvider.GetService<BrawlhallaStatContext>();
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        ConfigureApplication(app);

        app.Run();
    }
}