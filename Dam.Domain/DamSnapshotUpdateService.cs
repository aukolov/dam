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
            var bytes = _damExcelDownloader.TryDownload();
            if (bytes == null)
            {
                return;
            }

            var dataSet = _excelReader.Read(bytes);
            var snapshots = _dataSetToDamSnapshotTranslator.Translate(dataSet);
            _damSnapshotRepository.Update(snapshots);
        }
    }
}