using System;
using System.Reflection;
using Dam.Infrastructure.Excel;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Infrastructure.Excel
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
            ShouldBeTestExtensions.ShouldBe(dataSet.Tables.Count, 1);
            var table = dataSet.Tables[0];

            ShouldBeTestExtensions.ShouldBe<object>(table.Rows[16][1], "Kouris");
            ShouldBeTestExtensions.ShouldBe<object>(table.Rows[16][4], 115.0);
            ShouldBeTestExtensions.ShouldBe<object>(table.Rows[16][7], 9.2);
            ShouldBeTestExtensions.ShouldBe<object>(table.Rows[19][1], "Dipotamos");
            ShouldBeTestExtensions.ShouldBe<object>(table.Rows[19][4], 15.5);
            ShouldBeTestExtensions.ShouldBe<object>(table.Rows[19][7], 2.566);

            ShouldBeTestExtensions.ShouldBe<object>(table.Rows[9][11], new DateTime(2018, 1, 15));
        }
    }
}