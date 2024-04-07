using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using App.Core.Common.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions.Core;
using Telerik.Reporting.Cache;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;

namespace App.Core.Common;

public static class ServiceExtensions
{
    public static IServiceCollection AddScopedRepositories(this IServiceCollection services, Assembly assembly)
    {
        return services
            .AddScopedInterfaces("Repository", assembly);
    }

    public static IServiceCollection AddScopedServices(this IServiceCollection services, Assembly assembly)
    {
        return services
            .AddScopedInterfaces("Manager", assembly);
    }

    public static IServiceCollection AddInfrastructures(this IServiceCollection services)
    {
        return services.AddScoped<ICurrentContext, CurrentContext>()
            .AddScoped<IHttpCallService, HttpCallService>()
            .AddScoped<ICustomHttpClient, CustomHttpClient>();
    }

    public static IServiceCollection AddScopedInterfaceInstances<TInterface>(this IServiceCollection services,
        Assembly assembly)
    {
        var classTypes = assembly.GetTypes()
            .Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(TInterface)))
            .ToArray();

        foreach (var classType in classTypes) services.AddScoped(classType);

        return services;
    }

    public static IServiceCollection AddScopedInterfaces(this IServiceCollection services, string interfaceSuffix,
        Assembly assembly)
    {
        var classTypes = assembly.GetTypes()
            .Where(t => t.IsClass && t.Name.EndsWith(interfaceSuffix))
            .ToArray();

        foreach (var classType in classTypes)
        {
            var interfaceType = classType.GetInterface("I" + classType.Name);
            if (interfaceType != null) services.AddScoped(interfaceType, classType);
        }

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, AppSettingModel appConfig)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1.0", new OpenApiInfo { Title = appConfig.ApiName, Version = "1.0" });
        });
        services.AddSwaggerGen(setup =>
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });
        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, AppSettingModel appConfig)
    {
        app.UseSwagger(option => { option.PreSerializeFilters.Add((swaggerDoc, httpReq) => { }); });
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwaggerUI(c =>
        {
            //c.SwaggerEndpoint($"/swagger/v1.0/swagger.json",
            //    $"{appConfig.ApiName} API V1.0");
            foreach (var description in provider.ApiVersionDescriptions)
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            c.RoutePrefix = appConfig.BaseSwaggerEndpoint != null ? appConfig.BaseSwaggerEndpoint : "swagger";
        });

        return app;
    }

    public static IApplicationBuilder UseCustomApplicationBuilder(this IApplicationBuilder app,
        AppSettingModel appConfig, ILoggerFactory loggerFactory)
    {
        app.UseCustomLogger(loggerFactory);

        //app.UseHttpsRedirection();

        var filePath = appConfig.FileLocation.Path;
        if (string.IsNullOrWhiteSpace(filePath))
        {
            var rootPath = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            filePath = rootPath + Constants.FileBase.File;
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            //     FileProvider = new PhysicalFileProvider(filePath),
            //   RequestPath = Constants.FileBase.FileLink
        });
        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            // FileProvider = new PhysicalFileProvider(filePath),
            //RequestPath = Constants.FileBase.FileLink
        });
        app = UseCustomRoutingAndCros(app, appConfig);
        return app;
    }

    public static IApplicationBuilder UseCustomRoutingAndCros(this IApplicationBuilder app, AppSettingModel appConfig)
    {
        app.UseRouting();

        app.UseCors(x => x
            .SetIsOriginAllowed(origin => true)
            //.WithOrigins(appConfig.AllowOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        return app;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
        AppSettingModel appConfig)
    {
        services.AddAuthorization();
        var key = Encoding.ASCII.GetBytes(appConfig.Secret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                //appConfig.Authorize.Authority;
                //x.Audience = "";
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromDays(1)
                };
                x.Audience = string.Empty;
            });

        return services;
    }

    public static IServiceCollection AddCustomsTelerikService(this IServiceCollection services,
        IWebHostEnvironment webHostEnvironment)
    {
        services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
        );
        services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                IgnoreSerializableInterface = true,
                IgnoreSerializableAttribute = true,
                NamingStrategy = new CamelCaseNamingStrategy(true, false)
            };
        });

        services.TryAddSingleton<IReportServiceConfiguration>(sp =>
            new ReportServiceConfiguration
            {
                //ReportingEngineConfiguration = sp.GetService<IConfiguration>(),
                ReportingEngineConfiguration =
                    ConfigurationTelerikReportHelper.ResolveConfiguration(webHostEnvironment),
                HostAppId = "Net5RestServiceWithCors",
                //Storage = new FileStorage(),
                //Storage = new FileStorage(@"C:\TelerikReportFolder"),
                //Storage = new Telerik.Reporting.Cache.MsSqlServerStorage("Data Source=BINHHQ;Initial Catalog=TelerikReportCache;User ID=sa;Password=AAssdd12"),
                Storage = new MsSqlServerStorage("Data Source=10.12.2.232;Initial Catalog=TelerikReportCache;User ID=sa;Password=AAssdd12@123"),
                //
                ReportSourceResolver = new TypeReportSourceResolver()
                    .AddFallbackResolver(new UriReportSourceResolver(
                        Path.Combine(webHostEnvironment.ContentRootPath, "Reports")))
            });
        return services;
    }

    public static IServiceCollection AddCustomService(this IServiceCollection services, IConfiguration configuration,
        AppSettingModel appConfig)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressConsumesConstraintForFormFileParameters = true;
            options.SuppressInferBindingSourcesForParameters = true;
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

        services = AddCustomCrosService(services, appConfig);

        services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
        services.Configure<MvcOptions>(c => c.Conventions.Add(new ActionHidingConvention()));

        services.AddInfrastructures();
        services.Configure<AppSettingModel>(configuration);
        services.AddCustomSwagger(appConfig);
        services.AddCustomAuthentication(appConfig);
        services.AddCustomLogger(appConfig);
        return services;
    }

    public static IServiceCollection AddCustomCrosService(this IServiceCollection services, AppSettingModel appConfig)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                    //.WithOrigins(appConfig.AllowOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        return services;
    }


    public static IServiceCollection AddCustomLogger(this IServiceCollection services, AppSettingModel appConfig)
    {
        services.AddScoped(provider => provider.GetService<ILoggerFactory>().CreateLogger(appConfig.ApiName));
        var serviceName = new Regex(@"\W+").Replace(appConfig.ApiName, "");
        var logFormat = "BT - " + serviceName +
                        " {Level:u5} {Timestamp:yyyy-MM-dd HH:mm:ss} - {Message:j}{EscapedException}{NewLine}{NewLine}";
        var getPathLog = appConfig.FileLocation.LogPath;
        if (string.IsNullOrWhiteSpace(getPathLog))
        {
            var rootPath = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            getPathLog = rootPath + Constants.FileBase.LogPath;
        }

        var filePathFormat = $"{getPathLog}/{serviceName}" + "-{Date}.log";
        var retainedFileCountLimit = 10;

        var loggerConfig = new LoggerConfiguration();
        if (ApplicationMode.IsDevelopment)
            loggerConfig = loggerConfig
                .MinimumLevel.Is(LogEventLevel.Debug)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information);
        else
            loggerConfig = loggerConfig
                .MinimumLevel.Is(LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
        Log.Logger = loggerConfig
            .Enrich.With(new ExceptionEnricher())
            .WriteTo.RollingFile(filePathFormat
                , outputTemplate: logFormat
                , retainedFileCountLimit: retainedFileCountLimit
                , shared: true
            )
            .CreateLogger();

        services.AddLogging();

        return services;
    }

    public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder app, ILoggerFactory loggerFactory,
        bool logRequestInput = true)
    {
        loggerFactory.AddSerilog();

        if (logRequestInput) app.UseMiddleware<RequestInputLoggingMiddleware>();

        if (ApplicationMode.IsDevelopment)
            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    //.AddFilter("Microsoft", LogLevel.Warning)
                    //.AddFilter("System", LogLevel.Warning)
                    //.AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole()
                    .AddEventLog()
                    .AddDebug();
            });

        return app;
    }

    //Disabled Api Swagger of Telerik
    public class ActionHidingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerName.Contains("Designer")) action.ApiExplorer.IsVisible = false;
        }
    }
}