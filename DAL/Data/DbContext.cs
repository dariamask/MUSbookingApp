using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
