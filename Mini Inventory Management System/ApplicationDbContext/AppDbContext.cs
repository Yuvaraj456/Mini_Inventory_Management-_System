using Microsoft.EntityFrameworkCore;
using Mini_Inventory_Management_System.Models;
using System.Collections.Generic;

namespace Mini_Inventory_Management_System.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
            

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                        .HasData
                        (
                new Product     
                { 
                    Id = 1, 
                    Name = "Laptop", 
                    Price = 999.99, 
                    Stock = 10 
                }, 
                new Product 
                { 
                    Id = 2, 
                    Name = "Smartphone", 
                    Price = 599.99, 
                    Stock = 25 
                }, 
                new Product 
                { 
                    Id = 3, 
                    Name = "Tablet", 
                    Price = 299.99, 
                    Stock = 15 
                }, 
                new Product 
                { 
                    Id = 4, 
                    Name = "Headphones", 
                    Price = 49.99, 
                    Stock = 50 
                }, 
                new Product 
                { 
                    Id = 5, 
                    Name = "Smartwatch", 
                    Price = 199.99, 
                    Stock = 20 
                },
                new Product 
                { 
                    Id = 6, 
                    Name = "Monitor", 
                    Price = 179.99, 
                    Stock = 30 
                }, 
                new Product 
                { 
                    Id = 7, 
                    Name = "Keyboard", 
                    Price = 39.99, 
                    Stock = 80 
                }, 
                new Product 
                { 
                    Id = 8, 
                    Name = "Mouse", 
                    Price = 19.99, 
                    Stock = 100 
                }, 
                new Product 
                { 
                    Id = 9, 
                    Name = "Printer",
                    Price = 149.99, 
                    Stock = 10 
                },
                new Product
                {
                    Id = 10,
                    Name = "External Hard Drive",
                    Price = 89.99,
                    Stock = 60
                });

        }

    }
}
