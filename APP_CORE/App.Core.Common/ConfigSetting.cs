using System.IO;
using System.Reflection;
using App.Core.Common.Config;
using Microsoft.Extensions.Configuration;

namespace App.Core.Common;

public class ConfigSetting
{
    private ConfigSetting()
    {
    }

    public IConfigurationRoot Configuration { get; private set; }
    public AppSettingModel AppSetting { get; private set; }

    public static ConfigSetting Config(params string[] pathJson)
    {
        var environmentName = ApplicationMode.EnvironmentName;
        var exeFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().CodeBase);
        exeFolder = exeFolder.Replace(@"file:\", "");
        exeFolder = exeFolder.Replace(@"file:", "");
        var builder = new ConfigurationBuilder()
                .SetBasePath(exeFolder)
                .AddJsonFile("Config/appConfig.json", false, true)
                .AddJsonFile($"Config/appConfig{environmentName}.json")
                .AddJsonFile("Config/portConfig.json")
                .AddJsonFile("Config/lgspConfig.json")
                //.AddJsonFile($"appConfig.{environmentName}.json", false, true)
                .AddJsonFile("appsettings.json", true, true)
            ;
        foreach (var item in pathJson) builder.AddJsonFile(item);
        var config = builder.Build();

        var result = new ConfigSetting();
        result.Configuration = config;
        result.AppSetting = new AppSettingModel();
        config.Bind(result.AppSetting);

        return result;
    }
}