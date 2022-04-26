namespace Mongemini.Application.Core.Contracts
{
    public interface IPagedRequest
    {
        string Sort { get; }
        int Direction { get; }

        int? Page { get; }
        int? Size { get; }
    }
}
