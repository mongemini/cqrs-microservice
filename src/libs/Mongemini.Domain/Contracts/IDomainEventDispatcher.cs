using Mongemini.Domain.Entities;

namespace Mongemini.Domain.Contracts
{
    public interface IDomainEventDispatcher
    {
        Task Raise<TKey>(AggregateRoot<TKey> aggregate);
    }
}
