using App.Core.Common;
using App.QTHTGis.Services;
using Exceptionless;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.Text;

namespace App.QTHTGis.Api;

public class Startup : BaseStartup
{
    public Startup(IWebHostEnvironment env) : base(env)
    {
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        RuntimeConfiguration.AddConnectionString(nameof(AppConfig.ConnectionStrings.Qtht_Gis_ConnectionString),
            AppConfig.ConnectionStrings.Qtht_Gis_ConnectionString);
        ConfigLLBLRuntime.ConfigLLBLRuntimeConfiguration();

        services.AddScoped<ICurrentContext, CurrentContext>();
        services.AddScopedServices(ServiceAssembly.Assembly);
        //services.AddScopedRepositories(RepositoryAssembly.Assembly);
        services.AddCustomService(Configuration, AppConfig);

        // configure strongly typed settings objects
        services.AddAuthorization();
        var key = Encoding.ASCII.GetBytes(AppConfig.Secret);
        services.AddAuthentication(x => {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        //.AddJwtBearer(x => {
        //    //x.Authority = AppConfig.Authorize.Authority;
        //    x.RequireHttpsMetadata = false;
        //    x.SaveToken = true;
        //    x.TokenValidationParameters = new TokenValidationParameters {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        ClockSkew = TimeSpan.FromDays(1),
        //    };
        //    x.Audience = string.Empty;
        //});

        // config AddEndpointsApiExplorer AddExceptionless
        services.AddControllers();
        services.AddMvc();
        //services.AddEndpointsApiExplorer();
        services.AddApiVersioning(x => {
            x.DefaultApiVersion = new ApiVersion(1, 0);
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.ReportApiVersions = true;
            x.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        // Add ApiExplorer to discover versions
        services.AddVersionedApiExplorer(setup => {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        //services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<App.QthtGis.Services.Common.ConfigureSwaggerOptions>();
        services.AddExceptionless("xlVQQcybTb5G9SLUOeBo0DutmnLFEO2fvtfcfJ1T");
        services.AddMemoryCache();
        services.AddSignalR();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseCustomSwagger(AppConfig);
        }

        app.UseCustomApplicationBuilder(AppConfig, loggerFactory);
    }
}