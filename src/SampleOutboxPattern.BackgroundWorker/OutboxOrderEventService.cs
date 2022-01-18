using SampleOutboxPattern.BackgroundWorker.ReadRepositories;
using SampleOutboxPattern.Infrastructure.Messaging;

namespace SampleOutboxPattern.BackgroundWorker
{
    public class OutboxOrderEventService : BackgroundService
    {
        private readonly ILogger<OutboxOrderEventService> _logger;
        private readonly IntegrationOrderEventRepository _integrationOrderEventRepository;

        public OutboxOrderEventService(ILogger<OutboxOrderEventService> logger)
        {
            _logger = logger;
            _integrationOrderEventRepository = new IntegrationOrderEventRepository();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Read all available order events from the database
                var outboxOrderEvents = await _integrationOrderEventRepository.ReadOutboxUnprocessedOrderEventsAsync();
                _logger.LogInformation($"Starting Order Outbox integration for {outboxOrderEvents.Count()} events", DateTimeOffset.Now);

                //Iterate throughout all Outbox order events and try to publish to Kafka
                using (var producer =
                        new KafkaProducer<string, string>(
                            "order-events-v1",
                            "localhost:9092"))
                {
                    foreach (var item in outboxOrderEvents)
                    {
                        // Publish an event to Kafka
                        await producer.ProduceMessageAsync(item.Data, item.PartitionKey);
                        _logger.LogInformation($"Order Outbox event Key {item.PartitionKey} published to Kafka", DateTimeOffset.Now);

                        // Update the Outbox event ProcessedDate column witht the current processing datetime.
                        await _integrationOrderEventRepository.UpdateOutboxOrderEventAsProcessed(item.Id);
                        _logger.LogInformation($"Order Outbox event Key {item.PartitionKey} was processed!", DateTimeOffset.Now);
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
