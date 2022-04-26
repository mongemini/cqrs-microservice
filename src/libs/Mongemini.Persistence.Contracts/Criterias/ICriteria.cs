using System.Linq.Expressions;

namespace Mongemini.Persistence.Contracts.Criterias
{
    public interface ICriteria<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> Build();
    }
}
