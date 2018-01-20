using System;
using System.Collections.Generic;
using System.Linq;
using Dam.Domain;

namespace Dam.Infrastructure.DataAccess
{
    public class DamSnapshotRepository : IDamSnapshotRepository
    {
        private readonly Func<DataContext> _dataContextFactory;

        public DamSnapshotRepository(Func<DataContext> dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;

            RefreshItems();
        }

        public IReadOnlyCollection<DamSnapshot> Items { get; set; }

        public void Update(DamSnapshot[] snapshots)
        {
            using (var dataContext = _dataContextFactory())
            {
                var dates = snapshots.Select(x => x.Date).Distinct();
                var existingSnapshots = dataContext.Snapshots
                    .Where(x => dates.Contains(x.Date))
                    .ToArray();

                foreach (var snapshot in snapshots)
                {
                    var existingSnapshot = existingSnapshots
                        .SingleOrDefault(x => x.Dam == snapshot.Dam && x.Date == snapshot.Date);
                    if (existingSnapshot == null)
                    {
                        dataContext.Snapshots.Add(snapshot);
                    }
                }

                dataContext.SaveChanges();
            }
            RefreshItems();
        }

        private void RefreshItems()
        {
            using (var dataContext = _dataContextFactory())
            {
                Items = dataContext.Snapshots.ToArray();
            }
        }
    }
}