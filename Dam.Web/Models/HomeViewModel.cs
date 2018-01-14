using System.Collections.Generic;
using Dam.Domain;

namespace Dam.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Dams = new List<DamData>
            {
                new DamData("Kouris", 115000)
                {
                    Storage = 9194
                },
                new DamData("Kalavasos", 115000)
                {
                    Storage = 705
                },
                new DamData("Lefkara", 115000)
                {
                    Storage = 1497
                }
            };
        }

        public List<DamData> Dams { get; }
    }
}