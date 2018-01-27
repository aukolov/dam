using Dam.Domain;

namespace Dam.Web.Models
{
    public class DamRowViewModel
    {
        public DamRowViewModel(DamSnapshot currentSnapshot, DamSnapshot lastYearsSnapshot)
        {
            CurrentSnapshot = currentSnapshot;
            LastYearsSnapshot = lastYearsSnapshot;
        }

        public DamSnapshot CurrentSnapshot { get; }
        public DamSnapshot LastYearsSnapshot { get; }
    }
}