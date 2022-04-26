using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Mongemini.Persistence.Contracts.Data;


namespace Mongemini.Persistence.Implementations.Data
{
    public class DbConfigurator
    {
        private static readonly ConcurrentDictionary<Assembly, Type[]> ConfigurationTypes;

        static DbConfigurator()
        {
            ConfigurationTypes = new ConcurrentDictionary<Assembly, Type[]>();
        }

        public static void Register(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(a => !a.IsAbstract && !a.IsGenericType
                                                                     && typeof(IEntityTypeConfiguration).IsAssignableFrom(a)).ToArray();
            ConfigurationTypes.AddOrUpdate(assembly, types, (a, o) => o);
        }

        public static void Configure<TContext>(ModelBuilder builder) where TContext : DbContext
        {
            if (ConfigurationTypes.TryGetValue(typeof(TContext).Assembly, out var types))
            {
                Array.ForEach(types, t =>
                {
                    var obj = (IEntityTypeConfiguration)Activator.CreateInstance(t);
                    obj.Configure(builder);
                });
            }
        }
    }
}
