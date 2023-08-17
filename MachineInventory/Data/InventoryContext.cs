using MachineInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineInventory.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options) { }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>().ToTable("Machine");
            modelBuilder.Entity<Manufacturer>().ToTable("Manufacturer");
        }
    }
}
