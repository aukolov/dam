using System;

namespace Dam.Domain
{
    public class DamSnapshot
    {
        public long Id { get; set; }
        public DamEntity Dam { get; set; }
        public DateTime Date { get; set; }
        public decimal Storage { get; set; }

        public decimal StoragePercentage => (Storage / Dam.Capacity) * 100;
    }
}