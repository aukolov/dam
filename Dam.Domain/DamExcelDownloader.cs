using System.Text.RegularExpressions;

namespace Dam.Domain
{
    public class DamExcelDownloader
    {
        private const string ReservoirStatusRootUrl = "http://www.moa.gov.cy/moa/wdd/wdd.nsf/reservoir_en/";
        private const string ReservoirStatusPage = "reservoir_en";

        private readonly IDownloadService _downloadService;
        private readonly Regex _fileUrlRegex;

        public DamExcelDownloader(IDownloadService downloadService)
        {
            _downloadService = downloadService;
            _fileUrlRegex = new Regex("id=\"HotspotRectangle\\d+\" href=\"([^\"]+)\"");
        }

        public byte[] TryDownload()
        {
            var text = _downloadService.GetText(ReservoirStatusRootUrl + ReservoirStatusPage);
            if (text == null)
            {
                return null;
            }

            var match = _fileUrlRegex.Match(text);
            if (!match.Success)
            {
                return null;
            }
            if (match.Groups.Count != 2)
            {
                return null;
            }

            var fileUrl = ReservoirStatusRootUrl + match.Groups[1].Value;
            var bytes = _downloadService.DownloadBytes(fileUrl);
            return bytes;
        }
    }
}