using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mongemini.Persistence.Contracts.Data;
using System.Collections;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Data
{
    public class DbQuery<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _set;

        private readonly IQueryable<TEntity> _query;

        public DbQuery(DbSet<TEntity> set)
        {
            _set = set;
            _query = set;
        }

        public EntityEntry Attach(TEntity entity)
        {
            return _set.Attach(entity);
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return _set.AddAsync(entity, cancellationToken).AsTask();
        }

        public TEntity Find(object[] keyValues)
        {
            return _set.Find(keyValues);
        }

        public Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return _set.FindAsync(keyValues, cancellationToken).AsTask();
        }

        public Type ElementType => _query.ElementType;

        public Expression Expression => _query.Expression;

        public IQueryProvider Provider => _query.Provider;

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
