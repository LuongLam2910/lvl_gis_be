using App.Core.Common;
using App.Qtht.Services;
using App.Qtht.Services.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SD.LLBLGen.Pro.DQE.Oracle;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Text;
using Npgsql;
using Exceptionless;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Authorize.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment env) : base(env)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RuntimeConfiguration.AddConnectionString(nameof(AppConfig.ConnectionStrings.Auth_ConnectionString),
                                                     AppConfig.ConnectionStrings.Auth_ConnectionString);

            RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(
                c => c.AddDbProviderFactory(typeof(Npgsql.NpgsqlFactory)));

            //RuntimeConfiguration.ConfigureDQE<OracleDQEConfiguration>(c =>
            //{
            //    // add more here...
            //    c.AddDbProviderFactory(typeof(Oracle.ManagedDataAccess.Client.OracleClientFactory));
            //});

            //RuntimeConfiguration.ConfigureDQE<OracleDQEConfiguration>(
            //                c => c.AddSchemaNameOverwrite(AppConfig.SchemaOverride.Auth_Schema.FromSchema, AppConfig.SchemaOverride.Auth_Schema.ToSchema));
            //ConfigLLBLRuntime.ConfigLLBLRuntimeConfiguration();
            services.AddControllers();
            services.Configure<AppSettingModel>(Configuration);
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddScoped<ICurrentContext, CurrentContext>();
            services.AddScopedServices(ServiceAssembly.Assembly);

            // configure strongly typed settings objects
            services.AddAuthorization();
            var key = Encoding.ASCII.GetBytes(AppConfig.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                //x.Authority = AppConfig.Authorize.Authority;
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromDays(1),
                };
                x.Audience = string.Empty;
            });

            services.AddScoped<ICurrentContext, CurrentContext>();
            services.AddScopedServices(ServiceAssembly.Assembly);
            services.AddEndpointsApiExplorer();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
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

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            services.AddExceptionless("xlVQQcybTb5G9SLUOeBo0DutmnLFEO2fvtfcfJ1T");
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCustomSwagger(AppConfig);
            }
            app.UseExceptionless();
            app.UseCustomApplicationBuilder(AppConfig, loggerFactory);
        }
    }
}