using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Timers;
using App.CongAnGis.Services;
using App.Core.Common;
using DocumentFormat.OpenXml.Drawing;
using Exceptionless;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Npgsql;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using SD.LLBLGen.Pro.ORMSupportClasses;
using static App.Core.Common.Constants;
//using AppWithScheduler.Code;
//using AppWithScheduler.Code.Scheduling;
using App.CongAnGis.Services.Hubs;

namespace App.CongAnGis.Api;

public class Startup : BaseStartup
{
    public Startup(IWebHostEnvironment env) : base(env)
    {
    }

    public static void OnEventExecution(object sender, ElapsedEventArgs eventArgs)
    {
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddControllersWithViews()
            .AddNewtonsoftJson(c => c.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        services.AddControllers()
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        App.CongAnGis.Services.Common.Configuration.LLBLRuntimeConfiguration();
        services.AddScopedServices(ServiceAssembly.Assembly);
        services.AddCustomService(Configuration, AppConfig);
        services.AddCustomsTelerikService(webHostEnvironment);
        services.AddMvc();
        services.AddApiVersioning(x =>
        {
            x.DefaultApiVersion = new ApiVersion(1, 0);
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.ReportApiVersions = true;
            x.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        // Add ApiExplorer to discover versions
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        //services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<App.CongAnGis.Services.Common.ConfigureSwaggerOptions>();
        services.AddExceptionless("xlVQQcybTb5G9SLUOeBo0DutmnLFEO2fvtfcfJ1T");
        services.AddMemoryCache();


        string rootPath = Directory.GetDirectoryRoot(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        string path = rootPath + FileBase.File + AppConfig.FileLocation.Path;

        services.AddSingleton(path);
        services.AddSignalR();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseCustomSwagger(AppConfig);
        }

        app.UseExceptionless();
        app.UseCustomApplicationBuilder(AppConfig, loggerFactory);

        string rootPath = Directory.GetDirectoryRoot(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        string path = rootPath + FileBase.File + AppConfig.FileLocation.Path;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);    

        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(path),
            RequestPath = AppConfig.FileLocation.Path,
            EnableDefaultFiles = true
        });

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<SignalR>("/hub");
        });


    }
}