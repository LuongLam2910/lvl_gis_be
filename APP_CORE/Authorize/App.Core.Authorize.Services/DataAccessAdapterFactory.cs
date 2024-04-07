using App.Core.Common;
using App.Qtht.Dal.DatabaseSpecific;

namespace Authorize.Services
{
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
            return CreateAdapter(_configSetting.AppSetting.ConnectionStrings.Auth_ConnectionString);
        }
    }
}