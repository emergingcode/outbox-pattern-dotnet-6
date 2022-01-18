
using Dapper;

using OrderManagementApi.Models.Events;

namespace OrderManagementApi.Models
{
    [Table("Orders")]
    public class Order : AggregateRoot
    {
        private IList<OrderItem> _items;

        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public int TotalItems { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<OrderItem> Items
        {
            get => _items.ToList();
            set => _items = value.ToList();
        }

        public Order()
        {
            _items = new List<OrderItem>();

            //TODO: Verify the reason why the event wasn't created taking into consideration the items property.
            //      Another way can be to raise the event after persisting into the database.
            AddEvent(new PlacedOrder
            {
                Items = Items.ToDictionary(keySelector: m => m.ProductId, elementSelector: m => m.Quantity)
            });
        }

        public void AddItem(OrderItem item)
        {
            _items.Add(item);
        }
    }
}