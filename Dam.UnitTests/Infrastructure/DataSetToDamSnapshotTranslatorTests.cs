using System;
using System.Reflection;
using Dam.Domain;
using Dam.Infrastructure;
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
            var snapshot = _translator.Translate(dataSet);

            // Assert
            snapshot.DateTime.ShouldBe(new DateTime(2018, 1, 15));

            snapshot.Dams.Length.ShouldBe(18);
            var i = 0;
            AssertDam(snapshot.Dams[i++], "Kouris", 115m, 9.2m);
            AssertDam(snapshot.Dams[i++], "Kalavasos", 17.1m, 0.707m);
            AssertDam(snapshot.Dams[i++], "Lefkara", 13.85m, 1.503m);
            AssertDam(snapshot.Dams[i++], "Dipotamos", 15.5m, 2.566m);
            AssertDam(snapshot.Dams[i++], "Germasoyeia", 13.5m, 0.423m);
            AssertDam(snapshot.Dams[i++], "Arminou", 4.3m, 2.279m);
            AssertDam(snapshot.Dams[i++], "Polemidia", 3.4m, 0.696m);
            AssertDam(snapshot.Dams[i++], "Achna", 6.8m, 1.194m);
            AssertDam(snapshot.Dams[i++], "Asprokremmos", 52.375m, 10.07m);
            AssertDam(snapshot.Dams[i++], "Kannaviou", 17.168m, 2.947m);
            AssertDam(snapshot.Dams[i++], "Mavrokolympos", 2.18m, 0.644m);
            AssertDam(snapshot.Dams[i++], "Evretou", 24m, 7.016m);
            AssertDam(snapshot.Dams[i++], "Argaka", 0.99m, 0.388m);
            AssertDam(snapshot.Dams[i++], "Pomos", 0.86m, 0.302m);
            AssertDam(snapshot.Dams[i++], "Agia Marina", 0.298m, 0.108m);
            AssertDam(snapshot.Dams[i++], "Vyzakia", 1.69m, 0.013m);
            AssertDam(snapshot.Dams[i++], "Xyliatos", 1.43m, 0.211m);
            AssertDam(snapshot.Dams[i], "Kalopanagiotis", 0.363m, 0.222m);
        }

        private static void AssertDam(DamData dam, string name, decimal capacity, decimal storage)
        {
            dam.Name.ShouldBe(name);
            dam.Capacity.ShouldBe(capacity);
            dam.Storage.ShouldBe(storage);
        }
    }
}