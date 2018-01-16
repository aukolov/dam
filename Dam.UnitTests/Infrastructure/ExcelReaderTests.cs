using System;
using System.Reflection;
using Dam.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Infrastructure
{
    [TestFixture]
    public class ExcelReaderTests
    {
        private ExcelReader _excelReader;

        [SetUp]
        public void SetUp()
        {
            _excelReader = new ExcelReader();
        }

        [Test]
        public void ReadsExcel()
        {
            // Arrange
            byte[] bytes;
            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Dam.UnitTests.Resources.TestExcel.xls"))
            {
                stream.ShouldNotBeNull();
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }

            // Act
            var dataSet = _excelReader.Read(bytes);

            // Assert
            dataSet.Tables.Count.ShouldBe(1);
            var table = dataSet.Tables[0];

            table.Rows[16][1].ShouldBe("Kouris");
            table.Rows[16][4].ShouldBe(115.0);
            table.Rows[16][7].ShouldBe(9.2);
            table.Rows[19][1].ShouldBe("Dipotamos");
            table.Rows[19][4].ShouldBe(15.5);
            table.Rows[19][7].ShouldBe(2.566);

            table.Rows[9][11].ShouldBe(new DateTime(2018, 1, 15));
        }
    }
}