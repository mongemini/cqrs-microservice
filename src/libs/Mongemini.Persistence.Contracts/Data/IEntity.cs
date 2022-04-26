namespace Mongemini.Persistence.Contracts.Data
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
