using System;

namespace Dam.Domain
{
    public class DamData
    {
        private int _storage;

        public DamData(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public string Name { get; }
        public int Capacity { get; }

        public int Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                StoragePercentage = (decimal)Math.Round(Storage / (double)Capacity, 2);
            }
        }

        public decimal StoragePercentage { get; private set; }
    }
}
