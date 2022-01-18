namespace SampleOutboxPattern.Infrastructure.Domain
{
    public class AggregateRoot
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public IReadOnlyList<IEvent> Events => _events;

        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }
    }
}