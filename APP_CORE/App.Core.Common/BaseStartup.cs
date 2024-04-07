using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace App.Core.Common;

public class BaseStartup
{
    protected BaseStartup(IWebHostEnvironment env)
    {
        var configSetting = ConfigSetting.Config();
        AppConfig = configSetting.AppSetting;
        Configuration = configSetting.Configuration;
        webHostEnvironment = env;
    }

    protected AppSettingModel AppConfig { get; private set; }
    protected IConfiguration Configuration { get; set; }
    protected IWebHostEnvironment webHostEnvironment { get; set; }

    //protected IServiceCollection StandardServiceBuilder(IServiceCollection services)
    //{
    //    services.Configure<AppConfig>(Configuration);
    //    return services;
    //}

    //protected IServiceProvider BuildService(IServiceCollection services)
    //{
    //    var container = new ContainerBuilder();
    //    container.Populate(services);
    //    return new AutofacServiceProvider(container.Build());
    //}
}