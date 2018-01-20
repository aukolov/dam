using Dam.Infrastructure.DataAccess;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Infrastructure.DataAccess
{
    [TestFixture]
    public class DamRepositoryTests
    {
        private DamRepository _damRepository;

        [SetUp]
        public void SetUp()
        {
            DataAccessTestHelper.Initialize();
            _damRepository = new DamRepository(() => new DataContext());
        }

        [TearDown]
        public void TearDown()
        {
            DataAccessTestHelper.TryDeleteDatabase();
        }

        [Test]
        public void ProvidesAllDams()
        {
            _damRepository.Items.ShouldNotBeNull();
            _damRepository.Items.Count.ShouldBe(18);
        }
    }
}