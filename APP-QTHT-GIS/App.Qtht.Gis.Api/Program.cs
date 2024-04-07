using App.Core.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace App.QTHTGis.Api;

public class Program
{
    [Obsolete]
    public static void Main(string[] args)
    {
        ConfigSetting config = ConfigSetting.Config();
        CreateHostBuilder(config, args).Build().Run();
    }

    [Obsolete]
    public static IHostBuilder CreateHostBuilder(ConfigSetting config, string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseApplicationInsights()
                    .UseConfiguration(config.Configuration)
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .UseUrls($"http://0.0.0.0:{config.AppSetting.PortConfig.App_Qtht_Gis}")
                    ;
            });
    }
}