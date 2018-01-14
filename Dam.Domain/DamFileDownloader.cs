namespace Dam.Domain
{
    public class DamFileDownloader
    {
        private readonly IDownloadService _downloadService;

        public DamFileDownloader(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }

        public string Download()
        {
            var text = _downloadService.GetHtmlText();
            return null;
        }
    }
}