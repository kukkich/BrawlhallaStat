using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReplayWatcher.Desktop.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;
using ReplayWatcher.Desktop.Model.Authentication;
using ReplayWatcher.Desktop.Model.ReplayService;
using ReplayWatcher.Desktop.Model.Watcher;

namespace ReplayWatcher.Desktop;

public class Program
{
    [STAThread]
    public static void Main()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true
        });

        var app = provider.GetRequiredService<App>();

        try
        {
            app?.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        var appSettings = configuration.GetSection("AppSettings")
            .Get<AppConfiguration>()!;
        services.AddSingleton(appSettings);
        services.AddSingleton<Configuration.ConfigurationManager>();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        services.AddSingleton<IReplayService, LoggerReplayService>();
        services.AddSingleton<ReplayWatcherService>();
        services.AddTransient<JwtDelegatingHandler>();


        services.AddHttpClient("MyApiClient", (services, client) =>
        {
            var configuration = services.GetRequiredService<AppConfiguration>();
            client.BaseAddress = new Uri(configuration.ApiUrl);
        })
        .AddHttpMessageHandler<JwtDelegatingHandler>();

        services.AddSingleton<App>();
        services.AddSingleton<MainWindow>();
    }
}