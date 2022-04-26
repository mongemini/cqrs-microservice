using Mongemini.Persistence.Implementations.Criterias;
using Mongemini.Persistence.Implementations.Criterias.Extensions;
using Mongemini.Service.Infrastructure.EntitieCriterias;
using Mongemini.Service.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Mongemini.Service.Infrastructure.Filters
{
    public class BlankMainFilter : PagedCriteria<BlankEntity>
    {
        public string BlankFilter { get; set; }

        public override Expression<Func<BlankEntity, bool>> Build()
        {
            var criteria = True;
            if (!string.IsNullOrEmpty(BlankFilter))
            {
                criteria = criteria.And(new BlankSearch(BlankFilter));
            }

            if (string.IsNullOrEmpty(Sort))
            {
                Direction = 1;
                SetSortBy(s => s.Id);
            }

            return criteria.Build();
        }
    }
}
