namespace Dam.Domain
{
    public class DamSnapshotUpdateService : IDamSnapshotUpdateService
    {
        private readonly DamExcelDownloader _damExcelDownloader;
        private readonly IExcelReader _excelReader;
        private readonly IDataSetToDamSnapshotTranslator _dataSetToDamSnapshotTranslator;
        private readonly IDamSnapshotRepository _damSnapshotRepository;

        public DamSnapshotUpdateService(
            DamExcelDownloader damExcelDownloader,
            IExcelReader excelReader,
            IDataSetToDamSnapshotTranslator dataSetToDamSnapshotTranslator,
            IDamSnapshotRepository damSnapshotRepository)
        {
            _damExcelDownloader = damExcelDownloader;
            _excelReader = excelReader;
            _dataSetToDamSnapshotTranslator = dataSetToDamSnapshotTranslator;
            _damSnapshotRepository = damSnapshotRepository;
        }

        public void Update()
        {
            Global.Logger.Info("Downloading dam excel file.");
            var bytes = _damExcelDownloader.TryDownload();
            if (bytes == null)
            {
                Global.Logger.Info("Zero bytes downloaded.");
                return;
            }

            Global.Logger.Info("Reading Excel to dataset.");
            var dataSet = _excelReader.Read(bytes);
            Global.Logger.Info("Translating data sets to snapshots.");
            var snapshots = _dataSetToDamSnapshotTranslator.Translate(dataSet);
            Global.Logger.Info("Persisting snapshots.");
            _damSnapshotRepository.Update(snapshots);
            Global.Logger.Info("Done.");
        }
    }
}