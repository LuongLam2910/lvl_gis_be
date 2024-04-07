using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Common
{
    public class GetTokenNgsp
    {
        public string Url { get; set; }
        public string Key { get; set; }
    }

    public class GetTokenLgspBacGiang
    {
        public string Url { get; set; }
        public string Key { get; set; }
    }

    public class logEx
    {
        public Boolean Log { get; set; } = false;
    }

    public class PortConfig
    {
        public string Api_Gateway { get; set; }
        public string App_Dmht { get; set; }
        public string App_Qtht { get; set; }
        public string App_Qtht_Gis { get; set; }
        public string App_Auth { get; set; }
        public string App_Gopy { get; set; }
        public string App_DauTu { get; set; }
        public string App_TaiChinh { get; set; }
        public string App_SoNoiVu { get; set; }
        public string App_SoYTe { get; set; }
        public string App_SoGtvt { get; set; }
        public string App_NongNghiep { get; set; }
        public string App_CongAn { get; set; }
        public string App_CongAn_Gis { get; set; }
        public string App_Khcn { get; set; }
        public string App_SoVhttdl { get; set; }
        public string App_SoTuPhap { get; set; }
        public string App_Home { get; set; }
        public string App_Gddt { get; set; }
        public string App_Khdt { get; set; }
        public string App_Thanhtra { get; set; }
        public string TaiChinh_Data { get; set; }
        public string App_GIS { get; set; }
    }
    public class SchemaOverride
    {
        public Dmht_SchemaModel Dmht_Schema { get; set; }
        public Qtht_SchemaModel Qtht_Schema { get; set; }
        public Auth_SchemaModel Auth_Schema { get; set; }
        public Gopy_SchemaModel Gopy_Schema { get; set; }
        public DauTu_SchemaModel DauTu_Schema { get; set; }
        public TaiChinh_SchemaModel TaiChinh_Schema { get; set; }
        public SoNoiVu_SchemaModel SoNoiVu_Schema { get; set; }
        public SoYTe_SchemaModel SoYTe_Schema { get; set; }
        public SoGtvt_SchemaModel SoGtvt_Schema { get; set; }
        public NongNghiep_SchemaModel NongNghiep_Schema { get; set; }
        public CongAn_SchemaModel CongAn_Schema { get; set; }
        public CongAn_Gis_SchemaModel App_CongAn_Gis { get; set; }
        public Qtht_Gis_SchemaModel App_Qtht_Gis { get; set; }
        public Khcn_SchemaModel Khcn_Schema { get; set; }
        public SoTuPhap_SchemaModel SoTuPhap_Schema { get; set; }
        public Home_SchemaModel Home_Schema { get; set; }
        public SoVhttdl_SchemaModel SoVhttdl_Schema { get; set; }
        public Gddt_SchemaModel Gddt_Schema { get; set; }
        public Khdt_SchemaModel Khdt_Schema { get; set; }
        public Thanhtra_SchemaModel Thanhtra_Schema { get; set; }
        public TaiChinhData_SchemaModel TaiChinhData_Schema { get; set; }
        public CepLog_SchemaModel CepLog_Schema { get; set; }

        public class Khdt_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
        public class SoVhttdl_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class Khcn_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class CongAn_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class CongAn_Gis_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class Qtht_Gis_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class NongNghiep_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class SoGtvt_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class SoYTe_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class SoNoiVu_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
        public class Gopy_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class Auth_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class Qtht_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
        public class Dmht_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
        public class DauTu_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class TaiChinh_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
        public class SoTuPhap_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
        public class Home_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class Gddt_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class Thanhtra_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
        public class TaiChinhData_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }

        public class CepLog_SchemaModel
        {
            public string FromSchema { get; set; }
            public string ToSchema { get; set; }
        }
    }
    public class ConnectionStrings
    {
        public string Dmht_ConnectionString { get; set; }
        public string Qtht_ConnectionString { get; set; }
        public string Auth_ConnectionString { get; set; }
        public string Gopy_ConnectionString { get; set; }
        public string DauTu_ConnectionString { get; set; }
        public string TaiChinh_ConnectionString { get; set; }
        public string SoNoiVu_ConnectionString { get; set; }
        public string SoYTe_ConnectionString { get; set; }
        public string SoGtvt_ConnectionString { get; set; }
        public string NongNghiep_ConnectionString { get; set; }
        public string CongAn_ConnectionString { get; set; }
        public string CongAn_Gis_ConnectionString { get; set; }
        public string Qtht_Gis_ConnectionString { get; set; }
        public string Khcn_ConnectionString { get; set; }
        public string SoTuPhap_ConnectionString { get; set; }
        public string Home_ConnectionString { get; set; }
        public string SoVhttdl_ConnectionString { get; set; }
        public string Gddt_ConnectionString { get; set; }
        public string Khdt_ConnectionString { get; set; }
        public string Thanhtra_ConnectionString { get; set; }
        public string TaiChinhData_ConnectionString { get; set; }
        public string CepLog_ConnectionString { get; set; }
    }

    public class Authorize
    {
        public string DefaultScheme { get; set; }
        public string ClientName { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
    }

    public class FileLocation
    {
        public string Path { get; set; }
        public double MaxSize { get; set; }
        public string LogPath { get; set; }
    }

    public class ConfigAppGopY
    {
        public int PageIndexCount { get; set; }
        public int CommentsNewCount { get; set; }
    }

    public class ConfigAppCongAn
    {
        public int PageIndexCount { get; set; }
        public int CommentsNewCount { get; set; }
    }

    public class ApnNotificationSetting
    {
        public string AppBundleIdentifier { get; set; }
        public string P8PrivateKey { get; set; }
        public string P8PrivateKeyId { get; set; }
        public string TeamId { get; set; }
    }

    public class FcmNotificationSetting
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
    }

    public class VBQPPL
    {
        public string TimKiem { get; set; }
        public string GetAllLinhVuc { get; set; }
        public string GetAllCoQuanBienTap { get; set; }
        public string GetAllCoQuanPhatHanh { get; set; }
        public string GetById { get; set; }
        public string GetAllLoaiVanBan { get; set; }
        public string GetAllNguoiKy { get; set; }
        public string GetAllChucDanh { get; set; }
        public string TaiFileDinhKem { get; set; }
    }

    public class CASBG
    {

        public string providerUrl { get; set; }

        public string methodToken { get; set; }

        public string validAccessTokenMethod { get; set; }
        public string methodRevokeToken { get; set; }

        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string redirectUri { get; set; }

        public string AdminName { get; set; }
        public string AdminPass { get; set; }
    }

    public class Scheduler
    {
        public string NotificationForPoliceJob { get; set; }
        public string PingJob { get; set; }
        public string TemporaryCheckJob { get; set; }
    }

    public class SchedulerJob
    {
        public int? Hour { get; set; }
        public int? Minute { get; set; }
    }

    public class AESCryptSetting
    {
        public string IV { get; set; }
        public string Key { get; set; }
    }


    #region Declare AppConfig
    public partial class AppSettingModel
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public SchemaOverride SchemaOverride { get; set; }
        public PortConfig PortConfig { get; set; }
        public Authorize Authorize { get; set; }
        public string Secret { get; set; }
        public string ApiName { get; set; }
        public string ServiceProtocol { get; set; }
        public GetTokenNgsp GetTokenNgsp { get; set; }
        public GetTokenLgspBacGiang GetTokenLgspBacGiang { get; set; }
        public FileLocation FileLocation { get; set; }
        public ConfigAppCongAn ConfigAppCongAn { get; set; }
        public ConfigAppGopY ConfigAppGopY { get; set; }
        public string BaseSwaggerEndpoint { get; set; }
        public string BaseSwaggerEndpointDoc { get; set; }
        public string[] AllowOrigins { get; set; }

        //TODO: Kiendt add config for notification
        public FcmNotificationSetting FcmNotification { get; set; }
        public ApnNotificationSetting ApnNotification { get; set; }

        //TODO: Kiendt add config for search VBQPPL - LGSP BG
        public VBQPPL VBQPPL { get; set; }

        public CASBG CASBG { get; set; }
        public logEx LogEx { get; set; }
        public AppSettingModel()
        {
            LogEx = new logEx();
        }

        public string GoogleMapsAPI { get; set; }

        public string BingAPI { get; set; }

        public SchedulerJob SchedulerJob { get; set; }

        public Scheduler Scheduler { get; set; }

        public AESCryptSetting AESCryptSetting { get; set; }
    }
    #endregion
}
