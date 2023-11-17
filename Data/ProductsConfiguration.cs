using Microsoft.EntityFrameworkCore;
using ISM_Redesign.Models;
namespace ISM_Redesign.Data
{
    public class ProductConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Area).IsRequired();
                entity.Property(e => e.StockUniqueID).IsRequired();
                entity.Property(e => e.QuantityInStock).IsRequired();
                entity.Property(e => e.UnitMeasure).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.HasIndex(e => new { e.Name, e.UnitMeasure }).IsUnique();
                entity.HasIndex(e => e.StockUniqueID).IsUnique();
                // Add more configuration as needed
            });
        }
    }
}