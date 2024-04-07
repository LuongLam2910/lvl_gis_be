using App.ConganGis.Services.Manager.Jobs;
using App.Core.Common;
using DocumentFormat.OpenXml.EMMA;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Models.SysCauHinhModel;

namespace App.ConganGis.Services.Manager
{
    public interface ISchedulerManager
    {
        Task<bool> CreateJob(List<CauHinhResponseModel> model);
        Task<bool> CreateUserHistoryJob(string jobname);

        Task<IReadOnlyList<IScheduler>> GetAllCurrentJob();

        Task<bool> StopJob(string jobName, string jobGroup);
    }
    public class SchedulerManager : ISchedulerManager
    {
        private readonly ISchedulerFactory _schedulerFactory;


        public SchedulerManager(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }



        public async Task<bool> CreateJob(List<CauHinhResponseModel> model)
        {
            IScheduler sched = await _schedulerFactory.GetScheduler();
            //await sched.Start();

            // define the job and tie it to our HelloJob class
            int hour = int.Parse(DateTime.Now.ToString("HH"));
            var s = int.Parse(DateTime.Now.ToString("ss"));
            var m = int.Parse(DateTime.Now.ToString("mm"));
            IScheduler jobAll = await _schedulerFactory.GetScheduler();
            var seconds2 = ((hour * 60) * 60) + ((m + 1) * 60) + (s == 0 ? 5 : s);
            foreach (var item in model)
            {
                int startH = int.Parse(item.startTime.Split(":")[0]);
                int startM = int.Parse(item.startTime.Split(":").Length > 1 ? item.startTime.Split(":")[1] : "0");
                int endH = int.Parse(item.endTime.Split(":")[0]);
                int endM = int.Parse(item.endTime.Split(":").Length > 1 ? item.endTime.Split(":")[1] : "0");
                var start = ((startH * 60) + startM) * 60;
                var end = ((endH * 60) + endM) * 60;
                if (start <= seconds2 && seconds2 <= end)
                {
                    var check = jobAll.CheckExists(new JobKey("myJob" + item.id, "group1"));
                    if (!check.Result)
                    {
                        var repeat = int.Parse(item.tuanSuat) * 60;
                        IJobDetail job = JobBuilder.Create<Ping2>()
                                     .WithIdentity("myJob" + item.id, "group1")
                                     .UsingJobData("myJob", item.id.ToString())
                                     .Build();

                        // Trigger the job to run now, and then every 40 seconds
                        ITrigger trigger = TriggerBuilder.Create()
                             .WithIdentity("myTrigger" + item.id, "group1")
                             .StartNow()
                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(repeat).RepeatForever())
                             .Build();

                        await sched.ScheduleJob(job, trigger);
                    }
                }
                else
                {
                    var check = jobAll.CheckExists(new JobKey("myJob" + item.id, "group1"));
                    if (check.Result)
                    {
                        Console.WriteLine("deleteJob");
                       await sched.DeleteJob(new JobKey("myJob" + item.id, "group1"));
                    }
                }
            }
            return true;
        }

        public async Task<bool> CreateUserHistoryJob(string jobname)
        {
            IScheduler sched = await _schedulerFactory.GetScheduler();
            //await sched.Start();
            // define the job and tie it to our HelloJob class
            var check = sched.CheckExists(new JobKey(jobname, "userGroup"));
            if (!check.Result)
            {
                IJobDetail job = JobBuilder.Create<Ping2>()
                             .WithIdentity(jobname, "userGroup")
                             .UsingJobData("UserHistory", jobname)
                             .Build();

                // Trigger the job to run now, and then every 40 seconds
                ITrigger trigger = TriggerBuilder.Create()
                     .WithIdentity(jobname + "Trigger", "userGroup")
                     .StartNow()
                     .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                     .Build();

                await sched.ScheduleJob(job, trigger);
                Console.WriteLine($"{jobname} {trigger}");
            }
            return true;
        }

        public async Task<IReadOnlyList<IScheduler>> GetAllCurrentJob()
        {
            return await _schedulerFactory.GetAllSchedulers();
        }

        public async Task<bool> StopJob(string jobName, string jobGroup)
        {
            IScheduler sched = await _schedulerFactory.GetScheduler();
            var check = sched.CheckExists(new JobKey(jobName, "userGroup"));
            if (check.Result)
            {
                var x = await sched.DeleteJob(new JobKey(jobName, jobGroup));
                return x;
            }
            return false;
        }
    }
}
