using System;
using System.Linq;
using System.Reflection;
using Dam.Domain;
using Dam.Infrastructure.Excel;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Domain
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
            var damRepository = Substitute.For<IDamRepository>();

            DamEntity CreateDam(string name, decimal capacity)
            {
                return new DamEntity
                {
                    Name = name,
                    Capacity = capacity
                };
            }

            damRepository.Items.Returns(new[]
            {
                CreateDam("Kouris", 115m),
                CreateDam("Kalavasos", 17.1m),
                CreateDam("Lefkara", 13.85m),
                CreateDam("Dipotamos", 15.5m),
                CreateDam("Germasoyeia", 13.5m),
                CreateDam("Arminou", 4.3m),
                CreateDam("Polemidia", 3.4m),
                CreateDam("Achna", 6.8m),
                CreateDam("Asprokremmos", 52.375m),
                CreateDam("Kannaviou", 17.168m),
                CreateDam("Mavrokolympos", 2.18m),
                CreateDam("Evretou", 24m),
                CreateDam("Argaka", 0.99m),
                CreateDam("Pomos", 0.86m),
                CreateDam("Agia Marina", 0.298m),
                CreateDam("Vyzakia", 1.69m),
                CreateDam("Xyliatos", 1.43m),
                CreateDam("Kalopanagiotis", 0.363m)
            });
            _translator = new DataSetToDamSnapshotTranslator(damRepository);
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
            snapshots.Where((snapshot, index) => index % 2 == 0).Select(data => data.Date)
                .ShouldAllBe(time => time == new DateTime(2018, 1, 15));
            snapshots.Where((snapshot, index) => index % 2 == 1).Select(data => data.Date)
                .ShouldAllBe(time => time == new DateTime(2017, 1, 15));

            snapshots.Length.ShouldBe(2 * 18);
            var i = 0;
            AssertDam(snapshots[i++], "Kouris", 9.2m);
            AssertDam(snapshots[i++], "Kouris", 18.778m);
            AssertDam(snapshots[i++], "Kalavasos", 0.707m);
            AssertDam(snapshots[i++], "Kalavasos", 3.097m);
            AssertDam(snapshots[i++], "Lefkara", 1.503m);
            AssertDam(snapshots[i++], "Lefkara", 3.154m);
            AssertDam(snapshots[i++], "Dipotamos", 2.566m);
            AssertDam(snapshots[i++], "Dipotamos", 5.818m);
            AssertDam(snapshots[i++], "Germasoyeia", 0.423m);
            AssertDam(snapshots[i++], "Germasoyeia", 1.790m);
            AssertDam(snapshots[i++], "Arminou", 2.279m);
            AssertDam(snapshots[i++], "Arminou", 1.455m);
            AssertDam(snapshots[i++], "Polemidia", 0.696m);
            AssertDam(snapshots[i++], "Polemidia", 0.826m);
            AssertDam(snapshots[i++], "Achna", 1.194m);
            AssertDam(snapshots[i++], "Achna", 0.826m);
            AssertDam(snapshots[i++], "Asprokremmos", 10.07m);
            AssertDam(snapshots[i++], "Asprokremmos", 21.526m);
            AssertDam(snapshots[i++], "Kannaviou", 2.947m);
            AssertDam(snapshots[i++], "Kannaviou", 5.597m);
            AssertDam(snapshots[i++], "Mavrokolympos", 0.644m);
            AssertDam(snapshots[i++], "Mavrokolympos", 0.903m);
            AssertDam(snapshots[i++], "Evretou", 7.016m);
            AssertDam(snapshots[i++], "Evretou", 9.516m);
            AssertDam(snapshots[i++], "Argaka", 0.388m);
            AssertDam(snapshots[i++], "Argaka", 0.9m);
            AssertDam(snapshots[i++], "Pomos", 0.302m);
            AssertDam(snapshots[i++], "Pomos", 0.86m);
            AssertDam(snapshots[i++], "Agia Marina", 0.108m);
            AssertDam(snapshots[i++], "Agia Marina", 0.16m);
            AssertDam(snapshots[i++], "Vyzakia", 0.013m);
            AssertDam(snapshots[i++], "Vyzakia", 0.227m);
            AssertDam(snapshots[i++], "Xyliatos", 0.211m);
            AssertDam(snapshots[i++], "Xyliatos", 0.573m);
            AssertDam(snapshots[i++], "Kalopanagiotis", 0.222m);
            AssertDam(snapshots[i], "Kalopanagiotis", 0.363m);
        }

        private static void AssertDam(DamSnapshot snapshot, string name, decimal storage)
        {
            snapshot.Dam.Name.ShouldBe(name);
            snapshot.Storage.ShouldBe(storage);
        }
    }
}