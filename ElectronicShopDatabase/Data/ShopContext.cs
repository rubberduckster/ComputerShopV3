using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicShopDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ElectronicShopDatabase.Data
{
    public class ShopContext : DbContext
    {
        // Represents the Products table in the database
        public DbSet<Product> Products { get; set; }

        // Configure the SQLite database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connection string
            optionsBuilder.UseSqlite("Data Source=../../../electronicshop.db");
        }

        // Configure how Specifications is stored as JSON // Serialization and deserialization
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(product => product.Specifications)
            .HasConversion(
            specifications => JsonSerializer.Serialize(specifications, (JsonSerializerOptions?)null),
            json => JsonSerializer.Deserialize<Dictionary<string, string>>(json, (JsonSerializerOptions?)null)
        );
        }
    }
}
