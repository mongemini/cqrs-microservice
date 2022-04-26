namespace Mongemini.Persistence.Contracts.Criterias
{
    public interface IPageOptions
    {
        int? Size { get; }

        int? Page { get; }
    }
}
