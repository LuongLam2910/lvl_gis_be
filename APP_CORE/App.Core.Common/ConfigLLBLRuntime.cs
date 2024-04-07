using System.Diagnostics;
using Npgsql;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.Tools.OrmProfiler.Interceptor;
namespace App.Core.Common;

public static class ConfigLLBLRuntime
{
    public static void ConfigLLBLRuntimeConfiguration()
    {
        var configSetting = ConfigSetting.Config().AppSetting;
        RuntimeConfiguration.AddConnectionString(nameof(configSetting.ConnectionStrings.CongAn_ConnectionString),
           configSetting.ConnectionStrings.CongAn_ConnectionString);

        RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(c =>
        {
            c.SetTraceLevel(TraceLevel.Verbose)
                .AddDbProviderFactory(typeof(NpgsqlFactory))
                .AddDbProviderFactory(InterceptorCore.Initialize("App.CongAn.Api", typeof(NpgsqlFactory)))
                .SetCaseInsensitiveNamesFlag(true);
                //.c.AddSchemaNameOverwrite(AppConfig.SchemaOverride.CongAn_Schema.FromSchema,
                //    AppConfig.SchemaOverride.CongAn_Schema.ToSchema));
        });
        RuntimeConfiguration.Tracing
            .SetTraceLevel("ORMPersistenceExecution", TraceLevel.Verbose)
            .SetTraceLevel("ORMPlainSQLQueryExecution", TraceLevel.Verbose);

    }
}