using Confluent.Kafka;

namespace SampleOutboxPattern.Infrastructure.Messaging
{
    public class KafkaProducer<TKeyType, TEntity> : IDisposable
            where TEntity : class
    {
        private readonly string _topicName;

        private readonly ProducerConfig _producerConfig;

        public KafkaProducer(string topicName, string server)
        {
            _topicName = topicName;

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = server
            };
        }

        public async Task ProduceMessageAsync(TEntity entity, TKeyType partitionKey)
        {
            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.            
            using (var p = new ProducerBuilder<TKeyType, TEntity>(_producerConfig).Build())
            {
                var message = new Message<TKeyType, TEntity>
                {
                    Key = partitionKey,
                    Value = entity
                };

                await p.ProduceAsync(_topicName, message);
            }
        }

        public void Dispose()
        {
        }
    }
}
