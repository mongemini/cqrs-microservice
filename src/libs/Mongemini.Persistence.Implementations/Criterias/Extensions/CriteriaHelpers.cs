using Mongemini.Persistence.Contracts.Criterias;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias.Extensions
{
    public static class CriteriaHelpers
    {
        public static ICriteria<TEntity> And<TEntity>(this ICriteria<TEntity> from, ICriteria<TEntity> to) where TEntity : class
        {
            return to != null ? new CombinedCriteria<TEntity>(from, to) : from;
        }

        public static ICriteria<TEntity> Or<TEntity>(this ICriteria<TEntity> left, ICriteria<TEntity> right) where TEntity : class
        {
            return new OtherwiseCriteria<TEntity>(left, right);
        }

        public static ICriteria<TResult> Convert<TEntity, TResult>(this ICriteria<TEntity> from, Expression<Func<TResult, TEntity>> selector)
            where TEntity : class
            where TResult : class
        {
            return new TransitionCriteria<TEntity, TResult>(from, selector);
        }
    }
}
