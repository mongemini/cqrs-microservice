using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mongemini.Persistence.Contracts.Data
{
    public interface IDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IQueryable
        where TEntity : class
    {
        EntityEntry Attach(TEntity entity);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        TEntity Find(object[] keyValues);

        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
    }
}
