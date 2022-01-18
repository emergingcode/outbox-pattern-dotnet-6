namespace OrderManagementApi.Models.Events
{
    public class PlacedOrder : IEvent
    {
        public int OrderId { get; set; }

        public Dictionary<int, int> Items { get; set; }
    }
}
