using Newtonsoft.Json;

using SampleOutboxPattern.Infrastructure.Messaging;
using SampleOutboxPattern.Orders.Application.DataModels;
using SampleOutboxPattern.Orders.Application.Events;
using SampleOutboxPattern.Orders.Application.Models;
using SampleOutboxPattern.Orders.Application.Repositories;
using SampleOutboxPattern.Orders.Application.RequestModels;

namespace SampleOutboxPattern.Orders.Application
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly OutboxIntegrationOrderEventRepository _outboxIntegrationOrderEventRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _outboxIntegrationOrderEventRepository = new OutboxIntegrationOrderEventRepository();
        }
        
        public async Task PlaceCustomerOrder(CustomerOrderRequest customerOrderRequest)
        {
            Order customerOrder = new(customerOrderRequest.CustomerId, customerOrderRequest.Products);

            // Persists transactional data for a given CustomerOrder
            await _orderRepository.PlaceOrder(customerOrder);

            // Persists CustomerOrderEvent to outbox integration table
            foreach (CustomerOrderPlacedV1 customerOrderEvent in customerOrder.Events)
            {
                try
                {
                    string? fullName = customerOrderEvent.GetType().FullName;
                    string? name = customerOrderEvent.GetType().Name;
                    string? eventPartitionKey = customerOrderEvent.PartitionKey();
                    var data = JsonConvert.SerializeObject(customerOrderEvent, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    await _outboxIntegrationOrderEventRepository.RegisterPlacedOrderEventAsync(
                        new OutboxIntegrationOrderEventsData(customerOrderEvent.OccuredOn, fullName, name, eventPartitionKey, data));
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
