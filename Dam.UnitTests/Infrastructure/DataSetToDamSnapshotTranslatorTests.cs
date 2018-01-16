using System;
using System.Linq;
using System.Reflection;
using Dam.Domain;
using Dam.Infrastructure.Excel;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Infrastructure
{
    [TestFixture]
    public class DataSetToDamSnapshotTranslatorTests
    {
        private ExcelReader _excelReader;
        private DataSetToDamSnapshotTranslator _translator;

        [SetUp]
        public void SetUp()
        {
            _excelReader = new ExcelReader();
            _translator = new DataSetToDamSnapshotTranslator();
        }

        [Test]
        public void TranslatesDataSet()
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
            var dataSet = _excelReader.Read(bytes);

            // Act
            var snapshots = _translator.Translate(dataSet);

            // Assert
            snapshots.Select(data => data.DateTime).ShouldAllBe(time => time == new DateTime(2018, 1, 15));

            snapshots.Length.ShouldBe(18);
            var i = 0;
            AssertDam(snapshots[i++], "Kouris", 115m, 9.2m);
            AssertDam(snapshots[i++], "Kalavasos", 17.1m, 0.707m);
            AssertDam(snapshots[i++], "Lefkara", 13.85m, 1.503m);
            AssertDam(snapshots[i++], "Dipotamos", 15.5m, 2.566m);
            AssertDam(snapshots[i++], "Germasoyeia", 13.5m, 0.423m);
            AssertDam(snapshots[i++], "Arminou", 4.3m, 2.279m);
            AssertDam(snapshots[i++], "Polemidia", 3.4m, 0.696m);
            AssertDam(snapshots[i++], "Achna", 6.8m, 1.194m);
            AssertDam(snapshots[i++], "Asprokremmos", 52.375m, 10.07m);
            AssertDam(snapshots[i++], "Kannaviou", 17.168m, 2.947m);
            AssertDam(snapshots[i++], "Mavrokolympos", 2.18m, 0.644m);
            AssertDam(snapshots[i++], "Evretou", 24m, 7.016m);
            AssertDam(snapshots[i++], "Argaka", 0.99m, 0.388m);
            AssertDam(snapshots[i++], "Pomos", 0.86m, 0.302m);
            AssertDam(snapshots[i++], "Agia Marina", 0.298m, 0.108m);
            AssertDam(snapshots[i++], "Vyzakia", 1.69m, 0.013m);
            AssertDam(snapshots[i++], "Xyliatos", 1.43m, 0.211m);
            AssertDam(snapshots[i], "Kalopanagiotis", 0.363m, 0.222m);
        }

        private static void AssertDam(DamSnapshot snapshot, string name, decimal capacity, decimal storage)
        {
            snapshot.Dam.Name.ShouldBe(name);
            snapshot.Dam.Capacity.ShouldBe(capacity);
            snapshot.Storage.ShouldBe(storage);
        }
    }
}