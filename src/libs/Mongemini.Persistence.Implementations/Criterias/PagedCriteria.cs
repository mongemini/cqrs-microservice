using Mongemini.Persistence.Contracts.Criterias;

namespace Mongemini.Persistence.Implementations.Criterias
{
    public abstract class PagedCriteria<TEntity> : OrderCriteria<TEntity>, IPagedCriteria
        where TEntity : class
    {
        public const int DefaultPageSize = 10;

        public PagedCriteria()
        {
            Size = DefaultPageSize;
            Page = 1;
        }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}
