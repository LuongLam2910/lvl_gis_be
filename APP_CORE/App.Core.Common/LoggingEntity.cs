using System;
using App.QTHTGis.Dal.DatabaseSpecific;
using App.QTHTGis.Dal.EntityClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.Core.Common;

public static class LoggingEntity
{
    public static JsonSerializerSettings Settings = new()
    {
        MissingMemberHandling = MissingMemberHandling.Ignore,
        NullValueHandling = NullValueHandling.Include,
        DefaultValueHandling = DefaultValueHandling.Include,
        //settings for LLBLGen
        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        ContractResolver = new DefaultContractResolver
            { IgnoreSerializableInterface = true, IgnoreSerializableAttribute = true }
    };

    public static async void Log<TEntity>(this TEntity entity, decimal userId, string actionName, string comment,
        string IpClient, string userName, int? appId = null) where TEntity : EntityBase2
    {
        using (var adapter =
               new DataAccessAdapter(ConfigSetting.Config().AppSetting.ConnectionStrings.Qtht_Gis_ConnectionString))
        {
            //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
            var syslogAction = new SyslogactionEntity();
            syslogAction.IsNew = true;
            syslogAction.Action = actionName;
            syslogAction.DateCreate = DateTime.Now;
            syslogAction.ObjectName = typeof(TEntity).Name;
            var data = JsonConvert.SerializeObject(entity, Settings);
            syslogAction.Data = data;
            syslogAction.Note = comment;
            syslogAction.IpClient = IpClient;
            syslogAction.UserId = (long?)userId;
            syslogAction.UserName = userName;
            adapter.SaveEntity(syslogAction, true);
            //return GeneralCode.Success;
        }
    }

    public static void LogObject<TObject>(this TObject entity, decimal userId, string actionName, string comment,
        string IpClient, string userName, string appdinhdanh) where TObject : class
    {
        using (var adapter =
               new DataAccessAdapter(ConfigSetting.Config().AppSetting.ConnectionStrings.Qtht_Gis_ConnectionString))
        {
            // DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
            var syslogAction = new SyslogactionEntity();
            syslogAction.IsNew = true;
            syslogAction.Action = actionName;
            syslogAction.DateCreate = DateTime.Now;
            syslogAction.ObjectName = typeof(TObject).Name;
            var data = JsonConvert.SerializeObject(entity, Settings);
            syslogAction.Data = data;
            syslogAction.Note = comment;
            syslogAction.IpClient = IpClient;
            syslogAction.UserId = (long)userId;
            syslogAction.UserName = userName;
            adapter.SaveEntity(syslogAction);
            //return GeneralCode.Success;
        }
    }

    public static void LogObject<TObject>(this TObject entity, decimal userId, string actionName, string comment,
        string IpClient, string userName, int? appId = null) where TObject : class
    {
        using (var adapter =
               new DataAccessAdapter(ConfigSetting.Config().AppSetting.ConnectionStrings.Qtht_Gis_ConnectionString))
        {
            // DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
            var syslogAction = new SyslogactionEntity();
            syslogAction.IsNew = true;
            syslogAction.Action = actionName;
            syslogAction.DateCreate = DateTime.Now;
            syslogAction.ObjectName = typeof(TObject).Name;
            var data = JsonConvert.SerializeObject(entity, Settings);
            syslogAction.Data = data;
            syslogAction.Note = comment;
            syslogAction.IpClient = IpClient;
            syslogAction.UserId = (long?)userId;
            syslogAction.UserName = userName;
        }
    }
}