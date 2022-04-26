using Mongemini.Application.Core.ViewModels;
using Mongemini.Persistence.Contracts.Criterias;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Mongemini.Application.Core.Extensions
{
    public static class QueryableHelper
    {
        public static async Task<PagedList<TResult>> ToLookupAsync<TSource, TResult>(
              this IQueryable<TSource> source,
              IPagedCriteria options, IMapper mapper,
              CancellationToken cancellationToken) where TSource : class
              where TResult : class
        {
            var page = options.Page ?? 1;
            var size = options.Size ?? 20;
            var count = await source.CountAsync(cancellationToken);
            if (count == 0)
                return new PagedList<TResult>(new TResult[0], count, page, size);

            var items = await options.OrderBy(source)
                .Skip((page - 1) * size)
                .Take(size)
                .ProjectTo<TResult>(mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
            return new PagedList<TResult>(items, count, page, size);
        }
    }
}
