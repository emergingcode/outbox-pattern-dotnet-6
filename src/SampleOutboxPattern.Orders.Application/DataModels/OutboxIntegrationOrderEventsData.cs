using System.ComponentModel.DataAnnotations.Schema;

namespace SampleOutboxPattern.Orders.Application.DataModels
{
    [Table("OutboxIntegrationOrderEvents")]
    internal class OutboxIntegrationOrderEventsData
    {
        public Guid Id { get; }
        public string? FullName { get; }
        public string? Name { get; }
        public string? PartitionKey { get; }
        public string? Data { get; }
        public DateTime OccurredOn { get; }

        public OutboxIntegrationOrderEventsData(DateTime occurredOn, string? fullName, string? name,
            string? eventPartitionKey, string? data)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Name = name;
            PartitionKey = eventPartitionKey;
            Data = data;
            OccurredOn = occurredOn;
        }
    }
}
