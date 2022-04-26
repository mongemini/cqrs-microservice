using Mongemini.Persistence.Contracts.Criterias;
using Mongemini.Persistence.Implementations.Criterias.Extensions;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public class OtherwiseCriteria<TEntity> : ICriteria<TEntity> where TEntity : class
    {
        private readonly ICriteria<TEntity> _left;

        private readonly ICriteria<TEntity> _right;

        public OtherwiseCriteria(ICriteria<TEntity> left, ICriteria<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        public Expression<Func<TEntity, bool>> Build()
        {
            return _left.Build().Or(_right.Build());
        }
    }
}
