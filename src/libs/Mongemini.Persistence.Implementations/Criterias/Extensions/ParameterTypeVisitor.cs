using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Criterias.Extensions
{
    internal class ParameterTypeVisitor<TFrom, TTo> : ExpressionVisitor
    {
        private readonly Expression<Func<TTo, TFrom>> _selector;

        public ParameterTypeVisitor(Expression<Func<TTo, TFrom>> selector)
        {
            _selector = selector;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node.Type == typeof(TFrom) ? Visit(_selector.Body) : base.VisitParameter(node);
        }
    }
}
