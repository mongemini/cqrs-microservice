using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mongemini.Service.Infrastructure
{

    public class BlankContextFactory : IDesignTimeDbContextFactory<BlankContext>
    {
        public BlankContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlankContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=blank;");

            return new BlankContext(optionsBuilder.Options);
        }
    }
}
