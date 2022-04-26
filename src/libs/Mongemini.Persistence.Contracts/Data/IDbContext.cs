namespace Mongemini.Persistence.Contracts.Data
{
    public interface IDbContext : IDisposable
    {
        string ContextName { get; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();

        Task MigrateAsync(CancellationToken cancellationToken);

        Task<string[]> GetMigrationsAsync(CancellationToken cancellationToken);
    }
}
