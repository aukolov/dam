using System;

namespace Dam.Domain
{
    public class DamEntity
    {
        public DamEntity(
            string name,
            decimal capacity)
        {
            Name = name;
            Capacity = Math.Round(capacity, 3);
        }

        public string Name { get; }
        public decimal Capacity { get; }
    }
}
