using System;
using System.Collections.Generic;
using System.Linq;
using Dam.Domain;

namespace Dam.Infrastructure.DataAccess
{
    public class DamRepository : IDamRepository
    {
        public DamRepository(Func<IDataContext> dataContextFactory)
        {
            using (var dataContext = dataContextFactory())
            {
                Items = dataContext.Dams.ToArray();
            }
        }

        public IReadOnlyCollection<DamEntity> Items { get; }
    }
}