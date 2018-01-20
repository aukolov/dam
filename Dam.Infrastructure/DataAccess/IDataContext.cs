using System;
using Dam.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dam.Infrastructure.DataAccess
{
    public interface IDataContext : IDisposable
    {
        DbSet<DamEntity> Dams { get; set; }
        DbSet<DamSnapshot> Snapshots { get; set; }
    }
}