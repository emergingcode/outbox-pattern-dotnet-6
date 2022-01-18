using Dapper;

namespace SampleOutboxPattern.Orders.Application.Models
{
    [Table("OrderItems")]
    internal class OrderItem
    {
        public string? Sku { get; set; }

        public string? Name { get; set; }

        public int Quantity { get; set; }

        public decimal Value { get; set; }
    }
}
