using Mongemini.Domain.Entities;

namespace Mongemini.Domain.Contracts
{
    public interface IExternalDomainEventProvider
    {
        Task PublishAsync<T>(T externalEvent) where T : ExternalDomainEvent;
    }
}
