
using App.CongAnGis.Dal.DatabaseSpecific;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Services.ManagerBase;
using App.CongAnGis.Services.Model;
using App.Core.Common;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Model.SysLogModel;

namespace App.CongAnGis.Services.Manager
{
    public class SysLogsManager : SysimportLogsManagerBase
    {
        public interface ISysLogsManager
        {
            public Task<ApiResponse> insertLog(LogModel model);
        }
        public SysLogsManager()
        { }

        public async Task<ApiResponse> insertLog(LogModel model)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var data = new SyslogactiongisEntity();
                    data.Iddata = model.idData;
                    data.Data = model.data;
                    data.Note = model.note;
                    data.ObjectName = model.objectName;
                    data.UserId = model.user_id;
                    data.UserName = model.userName;
                    data.Action = model.action;
                    adapter.SaveEntityAsync(data);
                    return GeneralCode.Success;
                }
                catch (System.Exception ex)
                {

                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
                
            }
        }
    }
}

