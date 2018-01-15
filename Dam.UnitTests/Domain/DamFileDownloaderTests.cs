using System.IO;
using Dam.Domain;
using Dam.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Domain
{
    [TestFixture]
    public class DamFileDownloaderTests
    {
        private DamFileDownloader _downloader;

        [SetUp]
        public void SetUp()
        {
            var downloadService = new DownloadService();
            _downloader = new DamFileDownloader(downloadService);
        }

        [Test]
        public void DownloadsDamFile()
        {
            var result = _downloader.TryDownload();

            result.ShouldNotBeNull();
            result.Length.ShouldBeGreaterThan(50000);
            result.Length.ShouldBeLessThan(100000);
        }
    }
}
