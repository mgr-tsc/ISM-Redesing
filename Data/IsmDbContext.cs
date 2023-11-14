
using Microsoft.EntityFrameworkCore;

namespace ISM_Redesign.Data
{
    public class IsmDbContext : DbContext
    {
        public IsmDbContext(DbContextOptions<IsmDbContext> options) : base(options)
        {

        }
    }
}
