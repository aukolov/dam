using System.Collections.Generic;

namespace Dam.Domain
{
    public interface IDamRepository
    {
        IReadOnlyCollection<DamEntity> Items { get; }
    }
}