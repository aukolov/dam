using System;
using System.Linq;
using Dam.Domain;
using Dam.Infrastructure.DataAccess;
using NUnit.Framework;
using Shouldly;

namespace Dam.UnitTests.Infrastructure.DataAccess
{
    [TestFixture]
    public class DamSnapshotRepositoryTests
    {
        private DamSnapshotRepository _damSnapshotRepository;
        private DamRepository _damRepository;

        [SetUp]
        public void SetUp()
        {
            DataAccessTestHelper.Initialize();
            _damRepository = new DamRepository(() => new DataContext());
            _damSnapshotRepository = new DamSnapshotRepository(() => new DataContext());
        }

        private static DamSnapshot CreateSnapshot(DamEntity dam, DateTime date, decimal storage)
        {
            return new DamSnapshot
            {
                Dam = dam,
                Date = date,
                Storage = storage
            };
        }

        [Test]
        public void ItemsAreEmptyByDefault()
        {
            _damSnapshotRepository.Items.ShouldNotBeNull();
            _damSnapshotRepository.Items.ShouldBeEmpty();
        }

        [Test]
        public void CreatesNewSnapshotOnUpdate()
        {
            var dam = _damRepository.Items.Single(x => x.Name == "Kouris");

            _damSnapshotRepository.Update(new[]
            {
                CreateSnapshot(dam, new DateTime(2018, 1, 21), 11.22m)
            });

            _damSnapshotRepository.Items.Count.ShouldBe(1);
            var snapshot = _damSnapshotRepository.Items.Single();
            snapshot.Dam.Id.ShouldBe(dam.Id);
            snapshot.Date.ShouldBe(new DateTime(2018, 1, 21));
            snapshot.Storage.ShouldBe(11.22m);
        }

        [Test]
        public void UpdatesExistingSnapshotOnUpdate()
        {
            var dam = _damRepository.Items.Single(x => x.Name == "Kouris");
            _damSnapshotRepository.Update(new[]
            {
                CreateSnapshot(dam, new DateTime(2018, 1, 21), 11.22m)
            });

            _damSnapshotRepository.Update(new[]
            {
                CreateSnapshot(dam, new DateTime(2018, 1, 21), 22.33m)
            });

            _damSnapshotRepository.Items.Count.ShouldBe(1);
            var snapshot = _damSnapshotRepository.Items.Single();
            snapshot.Dam.Id.ShouldBe(dam.Id);
            snapshot.Date.ShouldBe(new DateTime(2018, 1, 21));
            snapshot.Storage.ShouldBe(22.33m);
        }

        [Test]
        public void CreatesNewSnapshotForAnotherDate()
        {
            var dam = _damRepository.Items.Single(x => x.Name == "Kouris");
            _damSnapshotRepository.Update(new[]
            {
                CreateSnapshot(dam, new DateTime(2018, 1, 21), 11.22m)
            });

            _damSnapshotRepository.Update(new[]
            {
                CreateSnapshot(dam, new DateTime(2018, 1, 22), 22.33m)
            });

            var snapshots = _damSnapshotRepository.Items.OrderBy(x => x.Date).ToArray();
            snapshots.Length.ShouldBe(2);
            snapshots[0].Dam.Id.ShouldBe(dam.Id);
            snapshots[0].Date.ShouldBe(new DateTime(2018, 1, 21));
            snapshots[0].Storage.ShouldBe(11.22m);
            snapshots[1].Dam.Id.ShouldBe(dam.Id);
            snapshots[1].Date.ShouldBe(new DateTime(2018, 1, 22));
            snapshots[1].Storage.ShouldBe(22.33m);
        }

        [Test]
        public void CreatesNewSnapshotForAnotherDam()
        {
            var dam1 = _damRepository.Items.Single(x => x.Name == "Kouris");
            var dam2 = _damRepository.Items.Single(x => x.Name == "Kalavasos");
            _damSnapshotRepository.Update(new[]
            {
                CreateSnapshot(dam1, new DateTime(2018, 1, 21), 11.22m)
            });

            _damSnapshotRepository.Update(new[]
            {
                CreateSnapshot(dam2, new DateTime(2018, 1, 21), 22.33m)
            });

            var snapshots = _damSnapshotRepository.Items.OrderBy(x => x.Dam.Name).ToArray();
            snapshots.Length.ShouldBe(2);
            snapshots[0].Dam.Id.ShouldBe(dam2.Id);
            snapshots[0].Date.ShouldBe(new DateTime(2018, 1, 21));
            snapshots[0].Storage.ShouldBe(22.33m);
            snapshots[1].Dam.Id.ShouldBe(dam1.Id);
            snapshots[1].Date.ShouldBe(new DateTime(2018, 1, 21));
            snapshots[1].Storage.ShouldBe(11.22m);
        }

    }
}