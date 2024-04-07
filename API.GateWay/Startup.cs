using System.IO;
using System.Reflection;
using App.Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using static App.Core.Common.Constants;

namespace Api.Gateway.Api;

public class Startup
{
    public Startup(IWebHostEnvironment env)
    {
        var configSetting = ConfigSetting.Config();
        AppConfig = configSetting.AppSetting;
    }

    private AppSettingModel AppConfig { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomCrosService(AppConfig);
        services.AddSignalR();
        services.AddOcelot();


        string rootPath = Directory.GetDirectoryRoot(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        string path = rootPath + FileBase.File + AppConfig.FileLocation.Path;

        services.AddSingleton(path);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        //app.UseCustomSwagger(AppConfig);
        app.UseCustomRoutingAndCros(AppConfig);

        string rootPath = Directory.GetDirectoryRoot(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        string path = rootPath + FileBase.File + AppConfig.FileLocation.Path;

        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(path),
            RequestPath = AppConfig.FileLocation.Path,
            EnableDefaultFiles = true
        });

        app.UseWebSockets();
        app.UseOcelot().Wait();
    }
}