using Dapper;

using SampleOutboxPattern.Infrastructure.Domain;
using SampleOutboxPattern.Orders.Application.RequestModels;
using SampleOutboxPattern.Orders.Application.Events;

namespace SampleOutboxPattern.Orders.Application.Models
{
    [Table("Orders")]
    internal class Order : AggregateRoot
    {
        private IList<OrderItem> _orderItems;

        public int Id { get; }
        public int CustomerId { get; }
        public int TotalItems => Products.Count();
        public decimal Total => Products.Sum(p => p.Quantity * p.Value);
        public DateTime CreatedAt { get; }
        public IEnumerable<OrderItem> Products
        {
            get => _orderItems.ToList();
            set => _orderItems = value.ToList();
        }

        public Order(int customerId, IList<ProductDto> productsDto)
        {
            Id = default;
            CustomerId = customerId;
            _orderItems = productsDto
                        .Select(p => new OrderItem
                        {
                            Sku = p.Sku,
                            Name = p.Name,
                            Quantity = p.Quantity,
                            Value = p.UnitPrice,
                        })
                        .ToList();

            CreatedAt = DateTime.UtcNow;

            AddEvent(new CustomerOrderPlacedV1(CustomerId, Products));
        }
    }
}
