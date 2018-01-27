using System.Collections.Generic;

namespace Dam.Domain
{
    public interface IDamSnapshotRepository
    {
        IReadOnlyCollection<DamSnapshot> Items { get; }
        void Update(DamSnapshot[] snapshots);
    }
}