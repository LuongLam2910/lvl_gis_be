using System;
using System.IO;
using App.Core.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Api.Gateway.Api;

public class Program
{
    [Obsolete]
    public static void Main(string[] args)
    {
        var config = ConfigSetting.Config("Gateway_v1.json");
        CreateHostBuilder(config, args).Build().Run();
    }

    [Obsolete]
    public static IHostBuilder CreateHostBuilder(ConfigSetting config, string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseApplicationInsights()
                    .UseConfiguration(config.Configuration)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .UseUrls($"http://0.0.0.0:{config.AppSetting.PortConfig.Api_Gateway}");
            });
    }
}