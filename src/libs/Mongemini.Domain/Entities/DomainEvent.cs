using Mongemini.Domain.Contracts;

namespace Mongemini.Domain.Entities
{
    public abstract class DomainEvent
    {
        public DomainEvent(bool isExternal)
        {
            IsExternal = isExternal;
        }

        public bool IsExternal { get; protected set; }

        public virtual Task CallExternalEvent(IExternalDomainEventProvider eventProvider)
        {
            return Task.CompletedTask;
        }
    }
}
