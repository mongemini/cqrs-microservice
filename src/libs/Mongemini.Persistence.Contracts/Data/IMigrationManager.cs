namespace Mongemini.Persistence.Contracts.Data
{
    public interface IMigrationManager
    {
        Task MigrateAsync(CancellationToken cancellationToken);
        Task<IDictionary<string, string[]>> GetPendingMigrationsAsync(CancellationToken cancellationToken);
    }
}
