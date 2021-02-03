using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repo
    {
        private readonly ILogger<Repo> _logger;
        private readonly ShopContext _shopContext;

        public DbSet<CustomUser> customUsers;
        public DbSet<Location> locations;
        public DbSet<Order> orders;
        public DbSet<Product> products;
        public DbSet<LocationInventory> locationInventories;
        public DbSet<Cart> carts;
        public DbSet<CartInventory> cartInventories;
        public DbSet<OrderInventory> orderInventories;

        public Repo(ShopContext shopContext, ILogger<Repo> logger)
        {
            _shopContext = shopContext;
            this.customUsers = _shopContext.CustomUsers;
            this.locations = _shopContext.Locations;
            this.orders = _shopContext.Orders;
            this.products = _shopContext.Products;
            this.locationInventories = _shopContext.LocationInventories;
            this.carts = _shopContext.Carts;
            this.cartInventories = _shopContext.CartInventories;
            this.orderInventories = _shopContext.OrderInventory;
            _logger = logger;
            ValidateLocationTable();
            ValidateProductTable();
            ValidateInventory();
        }
        public void ValidateLocationTable()
        {
            if (_shopContext.Locations.Count() == 0)
            {
                _shopContext.Locations.Add(new Location("New York, NY"));
                _shopContext.Locations.Add(new Location("Orlando, FL"));
                _shopContext.Locations.Add(new Location("Los Angeles, CA"));
                _shopContext.Locations.Add(new Location("Palo Alto, CA"));
                _shopContext.Locations.Add(new Location("Chicago, IL"));
            }
            _shopContext.SaveChanges();
        }
        public void ValidateProductTable()
        {
            if (_shopContext.Products.Count() == 0)
            {
                List<Product> productList = new List<Product>();
                string[] descriptions = { "Ball-in-a-Cup Game", "Left Shoe", "Broken Soldering Iron", "Wedge of Cheese", "Straightened Paperclip" };
                decimal[] prices = { 5.00m, 7.83m, 8.41m, 4.99m, 0.99m };
                for (int i = 0; i < descriptions.Length; i++)
                {
                    Product newProduct = new Product();
                    newProduct.Description = $"{descriptions[i]}";
                    newProduct.Price = prices[i];
                    _shopContext.Products.Add(newProduct);
                }
                _shopContext.SaveChanges();
            }

        }
        public void ValidateInventory()
        {
            if (_shopContext.LocationInventories.Count() == 0)
            {
                foreach (Location location in _shopContext.Locations)
                {
                    foreach (Product product in _shopContext.Products)
                    {
                        _shopContext.LocationInventories.Add(new LocationInventory(location.LocationId, product.ProductId, 99));
                    }
                }
                _shopContext.SaveChanges();
            }
        }
        public CustomUser GetUserById(string id)
        {
            var user = customUsers.FirstOrDefault(u => u.Id == id);
            return user;
        }
        public void CommitSave()
        {
            _shopContext.SaveChanges();
        }        
    }
}
