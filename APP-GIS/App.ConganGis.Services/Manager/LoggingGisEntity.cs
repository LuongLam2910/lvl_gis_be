using System;
using App.CongAnGis.Dal.DatabaseSpecific;
using App.CongAnGis.Dal.EntityClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.Core.Common;

public static class LoggingGisEntity
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

    public static async void Log(EntityBase2 entity, decimal userId, string actionName, string comment,
        string IpClient, string userName, string entittyName)
    {
        using (var adapter =
               new DataAccessAdapter(ConfigSetting.Config().AppSetting.ConnectionStrings.CongAn_Gis_ConnectionString))
        {
            //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
            var syslogAction = new SyslogactiongisEntity();
            syslogAction.IsNew = true;
            syslogAction.Action = actionName;
            syslogAction.DateCreate = DateTime.Now;
            syslogAction.ObjectName = entittyName;
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

    public static async void LogCustom(string entity, decimal userId, string actionName, string comment,
        string IpClient, string userName, string entittyName)
    {
        using (var adapter =
               new DataAccessAdapter(ConfigSetting.Config().AppSetting.ConnectionStrings.CongAn_Gis_ConnectionString))
        {
            //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
            var syslogAction = new SyslogactiongisEntity();
            syslogAction.IsNew = true;
            syslogAction.Action = actionName;
            syslogAction.DateCreate = DateTime.Now;
            syslogAction.ObjectName = entittyName;
            syslogAction.Data = entity;
            syslogAction.Note = comment;
            syslogAction.IpClient = IpClient;
            syslogAction.UserId = (long?)userId;
            syslogAction.UserName = userName;
            adapter.SaveEntity(syslogAction, true);
            //return GeneralCode.Success;
        }
    }

}