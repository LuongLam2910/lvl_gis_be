using App.ConganGis.Api.Scheduler;
using App.Core.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.CongAnGis.Api
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
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.
                    UseApplicationInsights()
                   .UseConfiguration(config.Configuration)
                   .UseKestrel()
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseIISIntegration()
                   .UseStartup<Startup>()
                   .UseUrls($"http://0.0.0.0:{config.AppSetting.PortConfig.App_GIS}")
                ;
                }).ConfigureServices(
                (hostContext, services) =>
                {
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();
                        q.AddJobAndTrigger<PingJob>(hostContext.Configuration);
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                });

    }
}
