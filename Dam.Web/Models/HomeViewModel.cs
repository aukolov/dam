using System.Collections.Generic;
using Dam.Domain;

namespace Dam.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Dams = new List<DamEntity>();
        }

        public List<DamEntity> Dams { get; }
    }
}