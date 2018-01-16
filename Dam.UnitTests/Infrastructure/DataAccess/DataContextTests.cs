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
            _dataContext = new DataContext();
        }

        [TearDown]
        public void TearDown()
        {
            _dataContext?.Dispose();
        }

        [Test]
        public void CreatesDamsOnInit()
        {
        }

        [Test]
        public void SnapshotsAreEmptyByDefault()
        {
            _dataContext.Snapshots.ShouldBeEmpty();
        }
    }
}