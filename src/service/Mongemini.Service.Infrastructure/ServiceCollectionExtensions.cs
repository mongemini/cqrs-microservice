using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongemini.Persistence.Contracts.Data;
using Mongemini.Persistence.Implementations.Data;
using Mongemini.Service.Infrastructure.Contracts;
using Mongemini.Service.Infrastructure.Repositories;
using System.Reflection;


namespace Mongemini.Service.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            DbConfigurator.Register(Assembly.GetExecutingAssembly());

            var optionsBuilder = new DbContextOptionsBuilder<BlankContext>();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(BlankContext)));

            services.AddTransient<IDbContext>(c => new BlankContext(optionsBuilder.Options));
            services.AddTransient<IBlankContext>(c => new BlankContext(optionsBuilder.Options));
            services.AddTransient<IBlankRepository, BlankRepository>();

            services.AddTransient<IMigrationManager, MigrationManager>();
        }
    }
}
