using System.Linq.Expressions;

namespace Mongemini.Persistence.Contracts.Data
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class, IEntity<TKey>
    {
        bool Any(TKey id);

        bool Any(Expression<Func<TEntity, bool>> criteria);

        int Count();

        int Count(Expression<Func<TEntity, bool>> criteria);

        IQueryable<TEntity> Get(TKey id);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> criteria);

        void Add(TEntity entity);

        void Update(TEntity entity);

        TEntity Update(TKey id, Action<TEntity> func, params Expression<Func<TEntity, dynamic>>[] propertyToUpdate);

        void Delete(TKey id);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        int Save();

        Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken);

        Task<bool> AnyAsync(TKey id, CancellationToken cancellationToken);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        Task<int> SaveAsync(CancellationToken cancellationToken);

        Task UpdateRangeAsync(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TEntity>> updateFactory, CancellationToken cancellationToken);

        Task RemoveRangeAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);

        Task ClearAsync(CancellationToken cancellationToken);
    }
}
