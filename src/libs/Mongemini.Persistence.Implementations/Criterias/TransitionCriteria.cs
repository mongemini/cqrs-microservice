using Mongemini.Persistence.Contracts.Criterias;
using Mongemini.Persistence.Implementations.Criterias.Extensions;
using System.Linq.Expressions;
using System.Text;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public class TransitionCriteria<TFrom, TTo> : ICriteria<TTo>
        where TTo : class
        where TFrom : class
    {
        private readonly ICriteria<TFrom> _source;

        private readonly Expression<Func<TTo, TFrom>> _selector;

        public TransitionCriteria(ICriteria<TFrom> criteria, Expression<Func<TTo, TFrom>> selector)
        {
            _source = criteria;
            _selector = selector;
        }

        public Expression<Func<TTo, bool>> Build()
        {
            return _source.Build().Convert(_selector);
        }
    }
}
