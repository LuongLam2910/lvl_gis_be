using App.Core.Common;
using App.QTHTGis.Dal.DatabaseSpecific;

namespace App.QTHTGis.Services;

public class DataAccessAdapterFactory
{
    private readonly ConfigSetting _configSetting;

    public DataAccessAdapterFactory()
    {
        _configSetting = ConfigSetting.Config();
    }

    private static DataAccessAdapter CreateAdapter(string connectionString)
    {
        return new DataAccessAdapter(connectionString);
    }

    public DataAccessAdapter CreateAdapter()
    {
        return CreateAdapter(_configSetting.AppSetting.ConnectionStrings.Qtht_Gis_ConnectionString);
    }
}