using App.CongAnGis.Services.Manager;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace App.ConganGis.Api.Scheduler
{
    public class PingJob : IJob
    {
        private readonly ILogger<PingJob> _logger;
        private readonly ISysCauHinhManager _CauHinhManager;
        
        public PingJob(ILogger<PingJob> logger, ISysCauHinhManager CauHinhManager)
        {
            _logger = logger;
            _CauHinhManager = CauHinhManager;
    }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Ping");
            _CauHinhManager.getAllCauHinhAsync();
            return Task.CompletedTask;
        }
    }

}
