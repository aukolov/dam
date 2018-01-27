using System;
using System.Collections.Generic;
using System.Linq;
using Dam.Domain;
using Dam.Infrastructure.DataAccess;
using Dam.Web.Extensions;

namespace Dam.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            var damSnapshotRepository = new DamSnapshotRepository(() => new DataContext());
            LatestSnapshots = damSnapshotRepository.Items.GroupBy(x => x.Dam)
                .Select(x => x.MaxBy(snapshot => snapshot.Date))
                .OrderByDescending(snapshot => snapshot.Dam.Capacity)
                .ToList();
            LastUpdate = LatestSnapshots.Max(snapshot => snapshot.Date);
        }

        public List<DamSnapshot> LatestSnapshots { get; }
        public DateTime LastUpdate { get; }
    }
}