using Microsoft.EntityFrameworkCore;
using Mongemini.Persistence.Contracts.Data;


namespace Mongemini.Persistence.Implementations.Data
{
    public class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
            Id = Guid.NewGuid().ToString();
        }

        public Task MigrateAsync(CancellationToken cancellationToken)
        {
            return Database.MigrateAsync(cancellationToken);
        }

        public async Task<string[]> GetMigrationsAsync(CancellationToken cancellationToken)
        {
            var migrations = await Database.GetPendingMigrationsAsync(cancellationToken);
            return migrations.ToArray();
        }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return new DbQuery<TEntity>(Set<TEntity>());
        }

        public string Id { get; private set; }

        private bool isDisposed;

        public string ContextName => GetType().Name;

        public override void Dispose()

        {
            if (isDisposed)
            {
                return;
            }

            base.Dispose();
            isDisposed = true;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            DetachAll();
            return result;
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();
            DetachAll();
            return result;
        }

        public int SaveChangesWithoutDetach()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesWithoutDetachAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        private void DetachAll()
        {
            var entires = ChangeTracker.Entries().ToArray();
            foreach (var entry in entires)
            {
                if (entry.Entity != null)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }
    }
}
