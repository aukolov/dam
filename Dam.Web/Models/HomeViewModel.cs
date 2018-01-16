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
                new DamData("Kouris", 115.000m, 9.194m),
                new DamData("Kalavasos", 17.100m, 7.05m),
                new DamData("Lefkara", 13.850m, 1.497m)
            };
        }

        public List<DamData> Dams { get; }
    }
}