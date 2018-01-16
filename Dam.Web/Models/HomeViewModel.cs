using System.Collections.Generic;
using Dam.Domain;

namespace Dam.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Dams = new List<DamEntity>
            {
                new DamEntity("Kouris", 115.000m),
                new DamEntity("Kalavasos", 17.100m),
                new DamEntity("Lefkara", 13.850m)
            };
        }

        public List<DamEntity> Dams { get; }
    }
}