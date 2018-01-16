using System;

namespace Dam.Domain
{
    public class DamSnapshotData
    {
        public DamSnapshotData(
            DateTime dateTime,
            DamData[] dams)
        {
            DateTime = dateTime;
            Dams = dams;
        }

        public DateTime DateTime { get; }

        public DamData[] Dams { get; }
    }
}