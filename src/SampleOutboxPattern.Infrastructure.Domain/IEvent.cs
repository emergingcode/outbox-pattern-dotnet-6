namespace SampleOutboxPattern.Infrastructure.Domain
{
    public interface IEvent
    {
        string PartitionKey();
    }
}
