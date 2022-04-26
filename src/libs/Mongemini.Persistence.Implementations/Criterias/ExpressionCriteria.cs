using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public class ExpressionCriteria<TEntity> : CriteriaBase<TEntity> where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _expression;

        public ExpressionCriteria(Expression<Func<TEntity, bool>> expression)
        {
            _expression = expression;
        }

        public override Expression<Func<TEntity, bool>> Build()
        {
            return _expression;
        }
    }
}
