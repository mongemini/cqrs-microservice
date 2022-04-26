using Mongemini.Persistence.Contracts.Criterias;
using Mongemini.Persistence.Implementations.Criterias.Extensions;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public abstract class OrderCriteria<TEntity> : CriteriaBase<TEntity>, ISortCriteria where TEntity : class
    {
        protected const string DefaultSortBy = "Id";

        public string Sort { get; set; }

        public int Direction { get; set; }

        public void SetSortBy<TProperty>(Expression<Func<TEntity, TProperty>> selector)
        {
            if (selector.Body is not MemberExpression memberEpression)
            {
                throw new NotSupportedException("SortBy expression is not supported");
            }

            Sort = memberEpression.Member.Name;
        }

        public IQueryable<T> OrderBy<T>(IQueryable<T> source) where T : class
        {
            return source.Provider.CreateQuery<T>(OrderyByExpression(source));
        }

        public IQueryable OrderBy(IQueryable source)
        {
            return source.Provider.CreateQuery(OrderyByExpression(source));
        }

        private Expression OrderyByExpression(IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (string.IsNullOrWhiteSpace(Sort))
            {
                Sort = DefaultSortBy;
            }

            var param = Expression.Parameter(source.ElementType, "p");
            var body = param.GetProperty(Sort);
            var orderByExp = Expression.Lambda(body, param);
            var typeArguments = new Type[] { source.ElementType, body.Type };
            return Expression.Call(
                typeof(Queryable),
                Direction > 0 ? "OrderBy" : "OrderByDescending",
                typeArguments,
                source.Expression,
                Expression.Quote(orderByExp));
        }
    }
}
