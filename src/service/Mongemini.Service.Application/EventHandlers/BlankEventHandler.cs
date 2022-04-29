using Mongemini.Bus.Contracts.Bus;
using Mongemini.Service.Application.Events;

namespace Mongemini.Service.Application.EventHandlers
{
    public class BlankEventHandler : IEventHandler<BlankEvent>
    {
        public Task Handle(BlankEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
