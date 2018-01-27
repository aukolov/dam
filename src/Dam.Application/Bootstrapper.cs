using System;
using System.Threading.Tasks;
using Dam.Domain;
using Dam.Infrastructure.DataAccess;
using Dam.Infrastructure.Excel;
using Dam.Infrastructure.Http;
using Dam.Infrastructure.Logging;

namespace Dam.Application
{
    public class Bootstrapper : IDisposable
    {
        private UpdateDamSnapshotsScheduler _updateDamSnapshotsScheduler;

        public void Start()
        {
            LogInitializer.InitializeLogger();

            Global.Logger.Info("Initializing database.");
            var databaseInitializer = new DatabaseInitializer();
            databaseInitializer.Initialize();

            Global.Logger.Info("Initializing snapshot update scheduler.");
            var damSnapshotUpdateService = new DamSnapshotUpdateService(
                new DamExcelDownloader(new DownloadService()),
                new ExcelReader(),
                new DataSetToDamSnapshotTranslator(new DamRepository(() => new DataContext())),
                new DamSnapshotRepository(() => new DataContext()));
            _updateDamSnapshotsScheduler = new UpdateDamSnapshotsScheduler(
                damSnapshotUpdateService);

            Global.Logger.Info("Strating scheduler database.");
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