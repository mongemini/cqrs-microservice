using Mongemini.Bus.Contracts.Events;

namespace Mongemini.Bus.Contracts.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
