using Mongemini.Persistence.Implementations.Criterias;
using Mongemini.Service.Infrastructure.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace Mongemini.Service.Infrastructure.EntitieCriterias
{
    public class BlankSearch : CriteriaBase<BlankEntity>
    {
        private readonly string _search;

        public BlankSearch(string search)
        {
            _search = search;
        }

        public override Expression<Func<BlankEntity, bool>> Build()
        {
            return entity => EF.Functions.Like(entity.Id, $"%{_search}%");
        }
    }
}
