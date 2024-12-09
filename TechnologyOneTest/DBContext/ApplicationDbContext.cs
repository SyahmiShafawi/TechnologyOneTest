using Microsoft.EntityFrameworkCore;
using TechnologyOneTest.DataModel;


namespace TechnologyOneTest
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        public DbSet<NumberCategory> NumberWords { get; set; }
    }
}
