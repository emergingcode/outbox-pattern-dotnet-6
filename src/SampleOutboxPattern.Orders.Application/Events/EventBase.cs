using SampleOutboxPattern.Infrastructure.Domain;

namespace SampleOutboxPattern.Orders.Application.Events
{
    public class EventBase: IEvent
    {
        private readonly Guid _eventId;

        public EventBase(string version)
        {
            Version = version;
            OccuredOn = DateTime.UtcNow;
            _eventId = Guid.NewGuid();
        }

        public string EventName => this.GetType().Name;

        public Guid EventId => _eventId;

        public string Version { get; }

        public DateTime OccuredOn { get; }

        public string PartitionKey() => $"{EventName}-{this.EventId}";
    }
}
