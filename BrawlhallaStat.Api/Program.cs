using System.Security.Claims;
using BrawlhallaReplayReader.DependencyInjection;
using BrawlhallaStat.Api.CommandHandlers.ReplayHandling;
using BrawlhallaStat.Api.Middlewares;
using BrawlhallaStat.Api.Services.Cache;
using BrawlhallaStat.Api.Services.Tokens;
using BrawlhallaStat.Domain.Context;
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
                .UseLoggerFactory(NullLoggerFactory.Instance); // for logging disable

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = TokenConfig.Issuer,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = TokenConfig.Audience,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = TokenConfig.GetSymmetricSecurityAccessKey(),
                };
            });

        services.AddAuthorization(option =>
        {
            option.AddPolicy("KukkichOnly", policy =>
            {
                policy.RequireClaim(ClaimTypes.Name, "kukkich");
            });
        });

        services.AddCors(options =>
        {
            options.AddPolicy(name: "WebClient",
                builder =>
                    builder.WithOrigins(
                            "http://localhost:8080",
                            "https://localhost:8080"
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

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddReplayHandlingPipeline();
        services.AddBrawlhallaReplayDeserializer();

        services.AddMemoryCache();
        services.AddCaching();

        services.AddTokenService();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        app.UseMiddleware<ApiExceptionMiddleware>();

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