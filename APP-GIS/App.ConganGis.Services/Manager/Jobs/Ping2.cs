using App.CongAnGis.Services.Hubs;
using App.CongAnGis.Services.Manager;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Models.SysCauHinhModel;
using static App.CongAnGis.Services.Models.SysUserHistory;

namespace App.ConganGis.Services.Manager.Jobs
{
    public class Ping2 : IJob
    {
        private readonly ISysCauHinhManager _CauHinhManager;
        IHubContext<SignalR> _hubContext;
       
        public Ping2(ISysCauHinhManager CauHinhManager, IHubContext<SignalR> hubContext)
        {
            _CauHinhManager = CauHinhManager;
            _hubContext = hubContext;
        }
        Task IJob.Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            if (dataMap.GetString("myJob") != null)
            {
                var id = int.Parse(dataMap.GetString("myJob"));
                Task<CauHinhResponseModel> data = _CauHinhManager.getOneById(id);
                _CauHinhManager.distancematrix(data.Result);
            } else if (dataMap.GetString("UserHistory") != null && dataMap.GetString("UserHistory") != "createLine")
            {
                var jobName = dataMap.GetString("UserHistory");
                var obj = new SysUserHistoryModel();
                obj.userId = int.Parse(jobName.Split("-")[1]);
                obj.userName = jobName.Split("-")[0];
                _hubContext.Clients.All.SendAsync("CreateUserHistory", System.Text.Json.JsonSerializer.Serialize(obj));
                Console.WriteLine(jobName);
            }
            
           
            return Task.CompletedTask;
        }
    }
}
