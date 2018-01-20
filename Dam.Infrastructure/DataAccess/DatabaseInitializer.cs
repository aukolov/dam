using System.Linq;
using Dam.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dam.Infrastructure.DataAccess
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        public void Initialize()
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Database.Migrate();
                LoadDefaultData(dataContext);
                dataContext.SaveChanges();
            }
        }

        private void LoadDefaultData(DataContext dataContext)
        {
            if (!dataContext.Dams.Any())
            {
                dataContext.Dams.Add(CreateDam("Kouris", 115m));
                dataContext.Dams.Add(CreateDam("Kalavasos", 17.1m));
                dataContext.Dams.Add(CreateDam("Lefkara", 13.85m));
                dataContext.Dams.Add(CreateDam("Dipotamos", 15.5m));
                dataContext.Dams.Add(CreateDam("Germasoyeia", 13.5m));
                dataContext.Dams.Add(CreateDam("Arminou", 4.3m));
                dataContext.Dams.Add(CreateDam("Polemidia", 3.4m));
                dataContext.Dams.Add(CreateDam("Achna", 6.8m));
                dataContext.Dams.Add(CreateDam("Asprokremmos", 52.375m));
                dataContext.Dams.Add(CreateDam("Kannaviou", 17.168m));
                dataContext.Dams.Add(CreateDam("Mavrokolympos", 2.18m));
                dataContext.Dams.Add(CreateDam("Evretou", 24m));
                dataContext.Dams.Add(CreateDam("Argaka", 0.99m));
                dataContext.Dams.Add(CreateDam("Pomos", 0.86m));
                dataContext.Dams.Add(CreateDam("Agia Marina", 0.298m));
                dataContext.Dams.Add(CreateDam("Vyzakia", 1.69m));
                dataContext.Dams.Add(CreateDam("Xyliatos", 1.43m));
                dataContext.Dams.Add(CreateDam("Kalopanagiotis", 0.363m));
            }
        }

        private DamEntity CreateDam(string name, decimal capacity)
        {
            return new DamEntity
            {
                Name = name,
                Capacity = capacity
            };
        }
    }
}