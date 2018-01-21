using System;
using System.Threading.Tasks;
using Dam.Domain;
using Dam.Infrastructure.DataAccess;
using Dam.Infrastructure.Excel;
using Dam.Infrastructure.Http;

namespace Dam.Application
{
    public class Bootstrapper : IDisposable
    {
        private UpdateDamSnapshotsScheduler _updateDamSnapshotsScheduler;

        public void Start()
        {
            var databaseInitializer = new DatabaseInitializer();
            databaseInitializer.Initialize();

            _updateDamSnapshotsScheduler = new UpdateDamSnapshotsScheduler(
                new DamSnapshotUpdateService(
                    new DamExcelDownloader(new DownloadService()),
                    new ExcelReader(),
                    new DataSetToDamSnapshotTranslator(),
                    new DamSnapshotRepository(() => new DataContext())));

            var task = _updateDamSnapshotsScheduler.Start();
            task.Wait(TimeSpan.FromSeconds(30));
            if (task.Status != TaskStatus.RanToCompletion)
            {
                throw new Exception("Failed to start scheduler.");
            }
        }

        public void Dispose()
        {
            _updateDamSnapshotsScheduler.Dispose();
        }
    }
}