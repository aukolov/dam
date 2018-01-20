using Dam.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dam.Infrastructure.DataAccess
{
    public class DataContext : DbContext, IDataContext
    {
        public const string DbFilePath = "dam.db";

        public DbSet<DamEntity> Dams { get; set; }
        public DbSet<DamSnapshot> Snapshots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbFilePath}");
        }
    }
}