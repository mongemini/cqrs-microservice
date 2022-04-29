using Mongemini.Bus.Contracts.Commands;
using Mongemini.Bus.Contracts.Events;

namespace Mongemini.Bus.Contracts.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
