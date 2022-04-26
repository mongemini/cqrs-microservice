using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mongemini.Persistence.Contracts.Data;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Mongemini.Persistence.Implementations.Data
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
               where TEntity : class, IEntity<TKey>
    {
        private readonly IDbContext _context;

        private readonly IDbSet<TEntity> _set;

        protected BaseRepository(IDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        protected IDbSet<TEntity> Set => _set;

        public virtual void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public bool Any(TKey id)
        {
            return Where(a => a.Id.Equals(id)).Any();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return Where(criteria).AnyAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(TKey id, CancellationToken cancellationToken)
        {
            return AnyAsync(a => a.Id.Equals(id), cancellationToken);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> criteria)
        {
            return Where(criteria).Any();
        }

        public int Count()
        {
            return GetAll().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return Where(criteria).Count();
        }

        public Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return GetAll().CountAsync(cancellationToken);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return GetAll().CountAsync(criteria, cancellationToken);
        }

        public virtual async Task AddOrUpdate(TEntity entity, CancellationToken cancellationToken)
        {
            if (!await AnyAsync(entity.Id, cancellationToken).ConfigureAwait(false))
            {
                await AddAsync(entity, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                Update(entity);
            }
        }

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return _set.AddAsync(entity, cancellationToken);
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            return Task.WhenAll(entities.Select(a => AddAsync(a, cancellationToken)).ToArray());
        }

        public virtual void Update(TEntity entity) { }

        public TEntity Update(TKey id, Action<TEntity> func, params Expression<Func<TEntity, dynamic>>[] propertyToUpdate)
        {
            var entity = CreateInstance(id);
            var entry = _set.Attach(entity);
            func(entity);
            if (propertyToUpdate.Length > 0)
            {
                ForceModified(entry, propertyToUpdate);
            }

            Update(entity);
            return entity;
        }

        protected virtual TEntity CreateInstance(TKey id)
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            return entity;
        }

        private static void ForceModified(EntityEntry entry, Expression<Func<TEntity, dynamic>>[] properties)
        {
            var list = properties.Select(a => GetPropertyByExpression(a.Body)).ToArray();
            Array.ForEach(list, p => entry.Property(p).IsModified = true);
        }

        private static string GetPropertyByExpression(Expression expression)
        {
            if (expression is UnaryExpression unaryExpression)
            {
                expression = unaryExpression.Operand;
            }

            return expression is not MemberExpression member
                ? throw new NotSupportedException("MemberExpressions are supported only.")
                : member.Member.Name;
        }

        public void Delete(TKey id)
        {
            var entity = CreateInstance(id);
            _set.Attach(entity);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _set.AsNoTracking();
        }

        public Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken)
        {
            return _set.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual IQueryable<TEntity> Get(TKey id)
        {
            return GetAll().Where(a => a.Id.Equals(id));
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public virtual Task UpdateRangeAsync(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TEntity>> updateFactory, CancellationToken cancellationToken)
        {
            return Where(criteria).UpdateAsync(updateFactory, cancellationToken);
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> criteria)
        {
            return GetAll().Where(criteria);
        }

        public virtual Task RemoveRangeAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return Where(criteria).DeleteAsync(cancellationToken);
        }

        public virtual Task ClearAsync(CancellationToken cancellationToken)
        {
            return GetAll().DeleteAsync(cancellationToken);
        }

        protected void Dispose(bool flag)
        {
            if (!flag)
            {
                return;
            }

            _context?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
