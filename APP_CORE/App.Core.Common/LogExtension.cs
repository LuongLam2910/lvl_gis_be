using System;
using System.Threading.Tasks;
using App.QTHTGis.Dal.DatabaseSpecific;
using App.QTHTGis.Dal.EntityClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.Core.Common;

public static class LogsMethod
{
    public static readonly string Login = "Login";
    public static readonly string Insert = "Insert";
    public static readonly string Update = "Update";
    public static readonly string Delete = "Delete";
}

public static class LogExtension
{
    private static readonly JsonSerializerSettings Settings = new()
    {
        MissingMemberHandling = MissingMemberHandling.Ignore,
        NullValueHandling = NullValueHandling.Include,
        DefaultValueHandling = DefaultValueHandling.Include,
        //settings for LLBLGen
        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        ContractResolver = new DefaultContractResolver
            { IgnoreSerializableInterface = true, IgnoreSerializableAttribute = true }
    };

    public static async void LogDefault<TObject>(this TObject entity, ICurrentContext currentContext, string method,
        string action) where TObject : class
    {
        await Log(entity, currentContext, method, action);
    }

    public static async void LogInsert<TObject>(this TObject entity, ICurrentContext currentContext)
        where TObject : class
    {
        await Log(entity, currentContext, LogsMethod.Insert, $"Insert {typeof(TObject).Name}");
    }

    public static async void LogUpdate<TObject>(this TObject entity, ICurrentContext currentContext)
        where TObject : class
    {
        await Log(entity, currentContext, LogsMethod.Update, $"Update {typeof(TObject).Name}");
    }

    public static async void LogDelete<TObject>(this TObject entity, ICurrentContext currentContext, string objName)
    {
        await Log(entity, currentContext, LogsMethod.Delete, $"Delete {objName}");
    }

    public static async void LogLogin<SysuserEntity>(this SysuserEntity entity, ICurrentContext currentContext)
    {
        await Log(entity, currentContext, LogsMethod.Login, "Login");
    }

    private static async Task Log<TObject>(this TObject entity, ICurrentContext currentContext, string actionName,
        string comment)
    {
        using (var adapter =
               new DataAccessAdapter(ConfigSetting.Config().AppSetting.ConnectionStrings.Qtht_ConnectionString))
        {
            var syslogAction = new SyslogactionEntity
            {
                IsNew = true,
                Action = actionName,
                DateCreate = DateTime.Now,
                ObjectName = typeof(TObject).Name,
                Data = JsonConvert.SerializeObject(entity, Settings),
                Note = comment,
                UserId = currentContext.UserId,
                UserName = currentContext.UserName
            };
            await adapter.SaveEntityAsync(syslogAction);
        }
    }
}