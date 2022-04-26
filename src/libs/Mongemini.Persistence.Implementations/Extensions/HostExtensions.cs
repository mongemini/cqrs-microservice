using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongemini.Persistence.Contracts.Data;

namespace Mongemini.Persistence.Implementations.Extensions
{
    public static class HostExtensions
    {
        public static Task MigrateDatabasesAsync(this IHost host, CancellationToken cancellationToken)
        {
            var migrationManager = host.Services.GetRequiredService<IMigrationManager>();
            return migrationManager.MigrateAsync(cancellationToken);
        }
    }
}
