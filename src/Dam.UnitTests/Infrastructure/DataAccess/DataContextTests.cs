using System.IO;
using System.Linq;
using Dam.Domain;
using Dam.Infrastructure.DataAccess;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Infrastructure.DataAccess
{
    [TestFixture]
    public class DataContextTests
    {
        private DataContext _dataContext;

        [SetUp]
        public void SetUp()
        {
            DataAccessTestHelper.Initialize();
            _dataContext = new DataContext();
        }

        [TearDown]
        public void TearDown()
        {
            _dataContext?.Dispose();
            DataAccessTestHelper.TryDeleteDatabase();
        }

        [Test]
        public void CreatesDamsOnInit()
        {
            _dataContext.Dams.Count().ShouldBe(18);
            var dams = _dataContext.Dams.OrderBy(entity => entity.Id).ToArray();

            AssertDam(dams, "Kouris", 115);
            AssertDam(dams, "Kalavasos", 17.1m);
            AssertDam(dams, "Lefkara", 13.85m);
            AssertDam(dams, "Dipotamos", 15.5m);
            AssertDam(dams, "Germasoyeia", 13.5m);
            AssertDam(dams, "Arminou", 4.3m);
            AssertDam(dams, "Polemidia", 3.4m);
            AssertDam(dams, "Achna", 6.8m);
            AssertDam(dams, "Asprokremmos", 52.375m);
            AssertDam(dams, "Kannaviou", 17.168m);
            AssertDam(dams, "Mavrokolympos", 2.18m);
            AssertDam(dams, "Evretou", 24m);
            AssertDam(dams, "Argaka", 0.99m);
            AssertDam(dams, "Pomos", 0.86m);
            AssertDam(dams, "Agia Marina", 0.298m);
            AssertDam(dams, "Vyzakia", 1.69m);
            AssertDam(dams, "Xyliatos", 1.43m);
            AssertDam(dams, "Kalopanagiotis", 0.363m);
        }

        private static void AssertDam(DamEntity[] dams, string name, decimal capacity)
        {
            dams.ShouldContain(entity => entity.Name == name);
            var dam = dams.Single(entity => entity.Name == name);
            dam.Capacity.ShouldBe(capacity);
        }

        [Test]
        public void SnapshotsAreEmptyByDefault()
        {
            _dataContext.Snapshots.ShouldBeEmpty();
        }
    }
}