using SampleOutboxPattern.Infrastructure.Messaging;
using SampleOutboxPattern.Orders.Application.Events;
using SampleOutboxPattern.Orders.Application.Models;
using SampleOutboxPattern.Orders.Application.Repository;
using SampleOutboxPattern.Orders.Application.RequestModels;

namespace SampleOutboxPattern.Orders.Application
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }
        
        public async Task PlaceCustomerOrder(CustomerOrderRequest customerOrderRequest)
        {
            Order customerOrder = new(customerOrderRequest.CustomerId, customerOrderRequest.Products);

            await _orderRepository.PlaceOrder(customerOrder);

            foreach (var customerOrderEvent in customerOrder.Events)
            {
                try
                {
                    using (var producer =
                            new KafkaProducer<string, CustomerOrderPlacedV1>(
                                "order-events-v1",
                                "localhost:9092"))
                    {
                        await producer.ProduceMessageAsync(/*(dynamic)*/(CustomerOrderPlacedV1)customerOrderEvent, customerOrderEvent.PartitionKey());
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
