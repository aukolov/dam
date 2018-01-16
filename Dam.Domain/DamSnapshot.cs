using System;

namespace Dam.Domain
{
    public class DamSnapshot
    {
        public DamSnapshot(
            DamEntity dam,
            DateTime dateTime,
            decimal storage)
        {
            Dam = dam;
            DateTime = dateTime;
            Storage = storage;
        }

        public DamEntity Dam { get; }
        public DateTime DateTime { get; }
        public decimal Storage { get; }
    }
}