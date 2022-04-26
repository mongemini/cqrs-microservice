using Mongemini.Persistence.Contracts.Criterias;
using Mongemini.Persistence.Implementations.Criterias.Extensions;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public class CombinedCriteria<TEntity> : ICriteria<TEntity> where TEntity : class
    {
        private readonly ICriteria<TEntity> _from;

        private readonly ICriteria<TEntity> _to;

        public CombinedCriteria(ICriteria<TEntity> from, ICriteria<TEntity> to)
        {
            _from = from;
            _to = to;
        }

        public Expression<Func<TEntity, bool>> Build()
        {
            return _from.Build().And(_to.Build());
        }
    }
}
