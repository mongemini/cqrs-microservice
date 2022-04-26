using Mongemini.Persistence.Contracts.Criterias;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public class EmptyCriteria<TEntity> : ICriteria<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Build()
        {
            return a => true;
        }
    }
}
