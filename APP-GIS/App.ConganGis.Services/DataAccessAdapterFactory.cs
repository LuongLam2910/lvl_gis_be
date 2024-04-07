using App.CongAnGis.Dal.DatabaseSpecific;
using App.Core.Common;

namespace App.CongAnGis.Services;

public class DataAccessAdapterFactory
{
    private readonly ConfigSetting _configSetting;

    public DataAccessAdapterFactory()
    {
        _configSetting = ConfigSetting.Config();
    }

    private static DataAccessAdapter CreateAdapter(string connectionString)
    {
        DataAccessAdapter adapter= new DataAccessAdapter(connectionString);
        adapter.CatalogNameToUse = "appcongangis";
        return adapter;
    }

    public DataAccessAdapter CreateAdapter()
    {
        return CreateAdapter(_configSetting.AppSetting.ConnectionStrings.CongAn_Gis_ConnectionString);
    }
}