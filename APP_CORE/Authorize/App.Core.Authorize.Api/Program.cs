using App.Core.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Authorize.Api
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var config = ConfigSetting.Config();
            CreateHostBuilder(config, args).Build().Run();
        }

        [Obsolete]
        public static IHostBuilder CreateHostBuilder(ConfigSetting config, string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.
                    UseApplicationInsights()
                   .UseConfiguration(config.Configuration)
                   .UseKestrel()
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseIISIntegration()
                   .UseStartup<Startup>()
                   .UseUrls($"https://0.0.0.0:{config.AppSetting.PortConfig.App_Auth}")
                ;
                });
    }
}