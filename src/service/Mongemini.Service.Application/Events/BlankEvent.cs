using Mongemini.Bus.Contracts.Events;

namespace Mongemini.Service.Application.Events
{
    public class BlankEvent : Event
    {
        public int From { get; private set; }
        public int To { get; private set; }
 
        public BlankEvent(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}
