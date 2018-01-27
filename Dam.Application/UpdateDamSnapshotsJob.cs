using System;
using System.Threading.Tasks;
using Dam.Domain;
using Quartz;

namespace Dam.Application
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UpdateDamSnapshotsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var damSnapshotUpdateService = (IDamSnapshotUpdateService)context.MergedJobDataMap["service"];
                damSnapshotUpdateService.Update();
            }
            catch (Exception e)
            {
                Global.Logger.Exception(e, "Exception while updating snapshots.");
            }

            return Task.FromResult(0);
        }
    }
}