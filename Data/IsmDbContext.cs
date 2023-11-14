
using Microsoft.EntityFrameworkCore;
using ISM_Redesign.Models;

namespace ISM_Redesign.Data
{
    public class IsmDbContext : DbContext
    {
        public IsmDbContext(DbContextOptions<IsmDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}

