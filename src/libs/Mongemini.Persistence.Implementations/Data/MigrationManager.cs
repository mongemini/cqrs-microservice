using Mongemini.Persistence.Contracts.Data;

namespace Mongemini.Persistence.Implementations.Data
{
    public class MigrationManager : IMigrationManager
    {
        public MigrationManager(IEnumerable<IDbContext> contexts)
        {
            _contexts = contexts;
        }
        private readonly IEnumerable<IDbContext> _contexts;

        public async Task MigrateAsync(CancellationToken cancellationToken)
        {
            var tasks = _contexts.Select(a => a.MigrateAsync(cancellationToken)).ToArray();
            await Task.WhenAll(tasks);
        }

        public async Task<IDictionary<string, string[]>> GetPendingMigrationsAsync(CancellationToken cancellationToken)
        {
            var tasks = _contexts.ToDictionary(a => a.ContextName, a => a.GetMigrationsAsync(cancellationToken));
            await Task.WhenAll(tasks.Values);
            return tasks.Select(a => new { a.Key, value = a.Value.Result }).ToDictionary(a => a.Key, a => a.value);
        }
    }
}
