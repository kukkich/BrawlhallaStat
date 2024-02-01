using System.IO;
using Newtonsoft.Json;

namespace ReplayWatcher.Desktop.Configuration;

public class ConfigurationManager
{
    public AppConfiguration Configuration { get; init; }

    public delegate void ConfigurationChangedDelegate(AppConfiguration newConfiguration);
    public event ConfigurationChangedDelegate OnConfigurationChanged = null!;

    public ConfigurationManager(AppConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void Update()
    {
        var json = JsonConvert.SerializeObject(Configuration, Formatting.Indented);
        File.WriteAllText("appsettings.json", json);
        OnConfigurationChanged.Invoke(Configuration);
    }
}