namespace SampleOutboxPattern.Orders.Application.RequestModels
{
    public class ProductDto
    {
        public string? Sku { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string? Name { get; set; }
    }
}
