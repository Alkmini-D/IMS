using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//made the sql server database and added the data in it
namespace IMS.Plugins.EFCore
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Assembly> Assemblies { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<AssemblyTransaction> AssemblyTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //build relationships
            modelBuilder.Entity<AssemblyInventory>()
                .HasKey(pi => new { pi.AssemblyId, pi.InventoryId });

            modelBuilder.Entity<AssemblyInventory>()
                .HasOne(pi => pi.Assembly)
                .WithMany(p => p.AssemblyInventories)
                .HasForeignKey(pi => pi.AssemblyId);

            modelBuilder.Entity<AssemblyInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(i => i.AssemblyInventories)
                .HasForeignKey(pi => pi.InventoryId);

            //seeding data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1,  InventoryName = "Gas Engine", Price = 1000, Quantity = 1 },
                new Inventory { InventoryId = 2,  InventoryName = "Body", Price = 400, Quantity = 1 },
                new Inventory { InventoryId = 3,  InventoryName = "Wheel", Quantity = 4, Price = 100 },
                new Inventory { InventoryId = 4,  InventoryName = "Seat", Price = 50, Quantity = 5 },
                new Inventory { InventoryId = 5,  InventoryName = "Electric Engine", Price = 8000, Quantity = 2 },
                new Inventory { InventoryId = 6,  InventoryName = "Battery", Price = 400, Quantity = 5 }
            );

            modelBuilder.Entity<Assembly>().HasData(
                new Assembly { AssemblyId = 1, AssemblyName = "Gas Car", Price = 20000, Quantity = 1 },
                new Assembly { AssemblyId = 2, AssemblyName = "Electric Car", Price = 15000, Quantity = 1 }
            );

            modelBuilder.Entity<AssemblyInventory>().HasData(
                new AssemblyInventory { AssemblyId = 1, InventoryId = 1, InventoryQuantity = 1 }, // engine
                new AssemblyInventory { AssemblyId = 1, InventoryId = 2, InventoryQuantity = 1 }, // body
                new AssemblyInventory { AssemblyId = 1, InventoryId = 3, InventoryQuantity = 4 }, //4 wheels
                new AssemblyInventory { AssemblyId = 1, InventoryId = 4, InventoryQuantity = 5 } //5 seats
            );

            modelBuilder.Entity<AssemblyInventory>().HasData(
                new AssemblyInventory { AssemblyId = 2, InventoryId = 5, InventoryQuantity = 1 }, // engine
                new AssemblyInventory { AssemblyId = 2, InventoryId = 2, InventoryQuantity = 1 }, // body
                new AssemblyInventory { AssemblyId = 2, InventoryId = 3, InventoryQuantity = 4 }, //4 wheels
                new AssemblyInventory { AssemblyId = 2, InventoryId = 4, InventoryQuantity = 5 }, //5 seats
                new AssemblyInventory { AssemblyId = 2, InventoryId = 6, InventoryQuantity = 1 } // battery
            );
        }
    }
}

