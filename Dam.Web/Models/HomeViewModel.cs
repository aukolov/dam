﻿using System.Collections.Generic;
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
                .ToList();
        }

        public List<DamSnapshot> LatestSnapshots { get; }
    }
}