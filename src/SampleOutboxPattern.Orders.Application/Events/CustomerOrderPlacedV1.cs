using SampleOutboxPattern.Orders.Application.Models;

namespace SampleOutboxPattern.Orders.Application.Events
{
    internal class CustomerOrderPlacedV1 : EventBase
    {
        public CustomerOrderPlacedV1(int customerId, IEnumerable<OrderItem> products)
        : base("1.0")
        {
            CustomerId = customerId;
            Products = products;
        }

        public int CustomerId { get; }
        public IEnumerable<OrderItem> Products { get; }
    }
}
