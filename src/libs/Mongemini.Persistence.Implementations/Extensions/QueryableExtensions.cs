using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mongemini.Persistence.Contracts.Criterias;
using Mongemini.Persistence.Implementations.Criterias.Extensions;
using System.Linq.Expressions;

namespace Mongemini.Persistence.Implementations.Extensions
{
    public static class QueryableExtensions
    {
        public static TResult[] ToArray<TSource, TResult>(this IQueryable<TSource> source, IMapper mapper)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider).ToArray();
        }

        public static Task<TResult[]> ToArrayAsync<TSource, TResult>(this IQueryable<TSource> source, IMapper mapper, CancellationToken cancellationToken)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider).ToArrayAsync(cancellationToken);
        }

        public static TResult SingleOrDefault<TSource, TResult>(this IQueryable<TSource> source, IMapper mapper)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider).SingleOrDefault();
        }

        public static Task<TResult> SingleOrDefaultAsync<TSource, TResult>(this IQueryable<TSource> source, IMapper mapper, CancellationToken cancellationToken)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
        }

        public static TResult FirstOrDefault<TSource, TResult>(this IQueryable<TSource> source, IMapper mapper)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider).FirstOrDefault();
        }

        public static Task<TResult> FirstOrDefaultAsync<TSource, TResult>(this IQueryable<TSource> source, IMapper mapper, CancellationToken cancellationToken)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        }

        public static Task<TResult> FirstOrDefaultAsync<TSource, TResult>(this IQueryable<TSource> source, IMapper mapper, Expression<Func<TResult, bool>> predicate, CancellationToken cancellationToken)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider).FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public static IOrderedQueryable<TSource> OrderByDirection<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, bool descending)
        {
            return descending ? source.OrderByDescending(selector)
                              : source.OrderBy(selector);
        }

        public static IOrderedQueryable<TSource> OrderByDirection<TSource>(this IQueryable<TSource> source, string property, bool descending)
        {
            return descending ? source.OrderByDescending(GetLambda<TSource>(property))
                              : source.OrderBy(GetLambda<TSource>(property));
        }

        private static Expression<Func<TSource, object>> GetLambda<TSource>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TSource), "p");
            var body = param.GetProperty(propertyName);
            return Expression.Lambda<Func<TSource, object>>(Expression.Convert(body, typeof(object)), param);
        }

        public static async Task<(List<TResult>, int)> ToLookupAsync<TSource, TResult>(
            this IQueryable<TSource> source,

            IPagedCriteria options,
            IMapper mapper,
            CancellationToken cancellationToken) where TSource : class
            where TResult : class
        {
            var page = options.Page ?? 1;
            var size = options.Size ?? 20;
            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            if (count == 0)
            {
                return (new List<TResult>(), 0);
            }

            var items = await options.OrderBy(source)
                .Skip((page - 1) * size)
                .Take(size)
                .ToArrayAsync<TSource, TResult>(mapper, cancellationToken)
                .ConfigureAwait(false);
            return (new List<TResult>(items), count);
        }
    }
}
