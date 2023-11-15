using Microsoft.EntityFrameworkCore;
using ISM_Redesign.Models;
using ISM_Redesing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ISM_Redesign.Data
{
    public class IsmDbContext : IdentityDbContext
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

