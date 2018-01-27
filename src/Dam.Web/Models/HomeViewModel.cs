using System;
using System.Collections.Generic;
using System.Linq;
using Dam.Infrastructure.DataAccess;
using Dam.Web.Extensions;

namespace Dam.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            var damSnapshotRepository = new DamSnapshotRepository(() => new DataContext());
            var latestSnapshots = damSnapshotRepository.Items.GroupBy(x => x.Dam)
                .Select(x => x.MaxBy(snapshot => snapshot.Date))
                .OrderByDescending(snapshot => snapshot.Dam.Capacity)
                .ToList();
            DamRows = latestSnapshots.Select(x => new DamRowViewModel(x,
                  damSnapshotRepository.Items
                  .SingleOrDefault(y => x.Dam == y.Dam && x.Date.AddYears(-1) == y.Date)))
                .ToList();
            LastUpdate = latestSnapshots.Max(snapshot => snapshot.Date);
        }

        public List<DamRowViewModel> DamRows { get; }
        public DateTime LastUpdate { get; }
    }
}