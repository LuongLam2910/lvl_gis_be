using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;

using App.Qtht.Services.Models;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.Linq;
using Microsoft.Extensions.Options;

namespace App.QTHTGis.Services.Manager;

public interface ILichSuDongBoManager
{
    Task<ApiResponse> ReloadDag(LichSuDongBoModel dag);
}

public class LichSuDongBoManager : ILichSuDongBoManager
{
    private readonly AppSettingModel _appSettings;
    private readonly ICurrentContext _currentContext;
    private readonly ICustomHttpClient _customHttpClient;

    public LichSuDongBoManager(ICurrentContext currentContext, ICustomHttpClient customHttpClient,
        IOptionsSnapshot<AppSettingModel> appConfig)
    {
        _currentContext = currentContext;
        _customHttpClient = customHttpClient;
        _appSettings = appConfig.Value;
    }

    public async Task<ApiResponse> ReloadDag(LichSuDongBoModel dag)
    {
        try
        {
            var qtht = new ApiNgspModel.QTHT(_appSettings.SyncQTHT_Api.Link, dag.DagId,
                _appSettings.LoginModel.UserName, _appSettings.LoginModel.PassWord);
            var headersOncePatch = new Dictionary<string, string>();
            headersOncePatch.Add("Authorization", qtht.Authorization);
            headersOncePatch.Add("Content-Type", "application/json");
            var objOncePatch = new { is_paused = dag.IsPaused };
            var resultOncePatch = _customHttpClient
                .PatchJsonAsync<object>(qtht.Patch, objOncePatch, null, headersOncePatch).Result;
            resultOncePatch.LogObject(_currentContext.UserId, Constants.UserAction.Sync, dag.TrangThai,
                _currentContext.IpClient, _currentContext.UserName, "APPQTHT");

            // Post Dag
            if (!dag.IsPaused)
            {
                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
                var headersOnceDag = new Dictionary<string, string>();
                headersOnceDag.Add("Authorization", qtht.Authorization);
                headersOnceDag.Add("Content-Type", "application/json");
                var objOnceDag = new { dag_run_id = Timestamp };
                var resultOnceDag = _customHttpClient
                    .PostJsonAsync<object>(qtht.PostDag, objOnceDag, null, headersOnceDag).Result;
                resultOnceDag.LogObject(_currentContext.UserId, Constants.UserAction.Sync, "ReloadDag_QTHT",
                    _currentContext.IpClient, _currentContext.UserName, "APPQTHT");
                if (dag.Id == 0)
                {
                    if (!dag.DagId.Contains("Once"))
                        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                        {
                            var entity = new LogdagEntity
                            {
                                Dagid = dag.DagId + "-Config",
                                Ngaychay = dag.DagId.Contains("Daily") ? DateTime.Now.AddDays(1).Date :
                                    dag.DagId.Contains("Weekly") ? NextWeekMonday() :
                                    dag.DagId.Contains("Mothly") ? NextMonthFirstDay() : null,
                                Tinhtrang = "Thêm mới cấu hình chạy",
                                Appid = dag.AppID,
                                IsNew = true,
                                IsDirty = true
                            };
                            await adapter.SaveEntityAsync(entity);
                        }
                }
                else
                {
                    using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var metan = new LinqMetaData(adapter);
                        var enity = metan.Logdag.FirstOrDefault(i => i.Id == dag.Id);
                        enity.Tinhtrang = "Đang chạy lại";
                        enity.Ngayketthuc = null;
                        enity.LogObject(_currentContext.UserId, Constants.UserAction.Update, "Update_LogDag",
                            _currentContext.IpClient, _currentContext.UserName, "APPQTHT");
                        await adapter.SaveEntityAsync(enity);
                    }
                }
            }

            return GeneralCode.Success;
        }
        catch (Exception ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    private DateTime NextWeekMonday()
    {
        //first get next monday
        var today = DateTime.Today;
        var daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
        //if today is monday, add seven days
        if (daysUntilMonday == 0) daysUntilMonday = 7;

        //create DateTime variables for next week's beginning and end
        var nextWeekMonday = today.AddDays(daysUntilMonday);

        return nextWeekMonday;
    }

    private DateTime NextMonthFirstDay()
    {
        var today = DateTime.Today;
        DateTime result;
        if (today.Month == 12) // its end of year , we need to add another year to new date:
            result = new DateTime(today.Year + 1, 1, 1);
        else
            result = new DateTime(today.Year, today.Month + 1, 1);
        return result;
    }
}