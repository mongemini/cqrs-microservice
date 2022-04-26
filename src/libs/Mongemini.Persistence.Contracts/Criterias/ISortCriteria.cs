namespace Mongemini.Persistence.Contracts.Criterias
{
    public interface ISortCriteria : ISortOptions
    {
        IQueryable<T> OrderBy<T>(IQueryable<T> source) where T : class;

        IQueryable OrderBy(IQueryable source);
    }
}
