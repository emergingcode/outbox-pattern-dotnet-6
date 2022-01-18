using System.ComponentModel.DataAnnotations.Schema;

namespace SampleOutboxPattern.BackgroundWorker.DataModels
{
    [Table("OutboxIntegrationOrderEvents")]
    internal class OutboxIntegrationOrderEventsData
    {
        public Guid Id { get; }
        public string PartitionKey { get; }
        public string Data { get; }

        public OutboxIntegrationOrderEventsData(Guid id, string partitionKey, string data)
        {
            Id = id;            
            PartitionKey = partitionKey;
            Data = data;
        }
    }
}
