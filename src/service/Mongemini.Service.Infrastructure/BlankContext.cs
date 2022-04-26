using Microsoft.EntityFrameworkCore;
using Mongemini.Persistence.Implementations.Data;
using Mongemini.Service.Infrastructure.Contracts;
using Mongemini.Service.Infrastructure.Entities;

namespace Mongemini.Service.Infrastructure
{
    public class BlankContext : BaseDbContext, IBlankContext
    {
        public BlankContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DbConfigurator.Configure<BlankContext>(modelBuilder);
        }

        public DbSet<BlankEntity> Blanks { get; set; }
    }
}
