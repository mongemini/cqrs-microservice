using Mongemini.Persistence.Contracts.Criterias;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public abstract class CriteriaBase<TEntity> : ICriteria<TEntity>
            where TEntity : class
    {
        public ICriteria<TEntity> True => new EmptyCriteria<TEntity>();

        public abstract Expression<Func<TEntity, bool>> Build();
    }
}
