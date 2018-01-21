using System;
using System.Threading.Tasks;
using Dam.Domain;
using Quartz;
using Quartz.Impl;

namespace Dam.Application
{
    public class UpdateDamSnapshotsScheduler : IDisposable
    {
        private readonly IDamSnapshotUpdateService _damSnapshotUpdateService;
        private IScheduler _scheduler;

        public UpdateDamSnapshotsScheduler(IDamSnapshotUpdateService damSnapshotUpdateService)
        {
            _damSnapshotUpdateService = damSnapshotUpdateService;
        }

        public async Task Start()
        {
            var factory = new StdSchedulerFactory();
            _scheduler = await factory.GetScheduler();

            await _scheduler.Start();
            
            var job = JobBuilder.Create<UpdateDamSnapshotsJob>()
                .WithIdentity("UpdateDamSnapshotsJob")
                .WithDescription("Downloads and merges latest dam snapshot info.")
                .UsingJobData(new JobDataMap
                {
                    {"service", _damSnapshotUpdateService}
                })
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("UpdateDamSnapshotsJobTrigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(79)
                    .RepeatForever())
                .StartNow()
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public void Dispose()
        {
            _scheduler?.Shutdown();
        }
    }
}