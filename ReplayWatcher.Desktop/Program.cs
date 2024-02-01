using Microsoft.Extensions.DependencyInjection;

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
        services.AddSingleton<App>();
        services.AddSingleton<MainWindow>();
    }
}