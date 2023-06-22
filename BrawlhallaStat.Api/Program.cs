using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Domain.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api;

public class Program
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BrawlhallaStatContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //.UseLoggerFactory(new NullLoggerFactory()) for logging disable
        });

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        using var scope = app.Services.CreateScope();

        var m = scope.ServiceProvider.GetService<IMediator>();
        m.Send(new TestMessageCommand("éîó"));

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