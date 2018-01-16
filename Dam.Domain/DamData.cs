using System;

namespace Dam.Domain
{
    public class DamData
    {
        public DamData(
            string name,
            decimal capacity,
            decimal storage)
        {
            Name = name;
            Capacity = Math.Round(capacity, 3);
            Storage = Math.Round(storage, 3);
            StoragePercentage = Math.Round(Storage / Capacity, 2);
        }

        public string Name { get; }
        public decimal Capacity { get; }
        public decimal Storage { get; }
        public decimal StoragePercentage { get; }
    }
}
