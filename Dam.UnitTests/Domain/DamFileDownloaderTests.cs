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
            var filePath = _downloader.Download();

            filePath.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            Path.GetExtension(filePath).ShouldBe(".xls");
        }
    }
}
