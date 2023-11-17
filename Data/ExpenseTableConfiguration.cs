using Microsoft.EntityFrameworkCore;
using ISM_Redesign.Models;
using ISM_Redesing.Models;
namespace ISM_Redesign.Data
{
    public class ExpenseTableConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.ExpenseID);
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Category).IsRequired();
                // Add more configuration as needed
            });
        }
    }
}