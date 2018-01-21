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
            var damSnapshotUpdateService = (IDamSnapshotUpdateService)context
                .MergedJobDataMap["service"];
            damSnapshotUpdateService.Update();
            var taskCompletionSource = new TaskCompletionSource<object>();
            taskCompletionSource.SetResult(null);
            return taskCompletionSource.Task;
        }
    }
}