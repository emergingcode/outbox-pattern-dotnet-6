namespace OrderManagementApi.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int LineNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
