using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mongemini.Persistence.Contracts.Data;

namespace Mongemini.Persistence.Implementations.Data
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        void IEntityTypeConfiguration.Configure(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder.Entity<TEntity>());
        }

        protected void ConfigurePrecision<TValue>(EntityTypeBuilder<TEntity> builder, string columnType)
        {
            var prop = typeof(TEntity).GetProperties().Where(a => a.CanRead && a.CanWrite && a.PropertyType == typeof(TValue)).ToList();
            foreach (var p in prop)
            {
                builder.Property(Type.GetType(columnType) ?? throw new InvalidOperationException(), p.Name);
            }
        }

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
