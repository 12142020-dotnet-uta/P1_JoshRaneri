using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class ShopContext : IdentityDbContext
    {
        public DbSet<CustomUser> CustomUsers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<LocationInventory> LocationInventories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartInventory> CartInventories { get; set; }
        public DbSet<OrderInventory> OrderInventory { get; set; }

        public ShopContext() { }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=P1_StoreDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationInventory>()
                .HasKey(c => new { c.LocationId, c.ProductId });
            modelBuilder.Entity<Cart>()
                .HasKey(c => new { c.CartId, c.Id, c.LocationId });
            modelBuilder.Entity<CartInventory>()
                .HasKey(c => new { c.CartId, c.ProductId });
            modelBuilder.Entity<OrderInventory>()
                .HasKey(c => new { c.OrderId, c.ProductId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
