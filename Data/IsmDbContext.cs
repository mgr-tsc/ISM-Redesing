using Microsoft.EntityFrameworkCore;
using ISM_Redesign.Models;
using ISM_Redesing.Models;

namespace ISM_Redesign.Data
{
    public class IsmDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public IsmDbContext(DbContextOptions<IsmDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        public static void InitializeDbTest(IApplicationBuilder applicationBuilder)
        {
            // code to thes integration with EF core goes here
        }
    }
}

