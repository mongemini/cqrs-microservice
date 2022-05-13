using Microsoft.EntityFrameworkCore;
using Mongemini.Service.Infrastructure;
using Mongemini.Service.Infrastructure.Contracts;
using Mongemini.Service.Infrastructure.Repositories;

namespace Mongemini.Tests
{
    public class TestHelper
    {
        public static IBlankContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<BlankContext>();
            builder.UseInMemoryDatabase(databaseName: "BaseDbContextInMemory");

            var dbContextOptions = builder.Options;
            var libraryDbContext = new BlankContext(dbContextOptions);
            
            libraryDbContext.Database.EnsureDeleted();
            libraryDbContext.Database.EnsureCreated();

            return libraryDbContext;
        }

        public static BlankRepository GetRepository()
        {
            return new BlankRepository(GetContext());
        }
    }
}
