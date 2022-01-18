namespace SampleOutboxPattern.Orders.Application.RequestModels
{
    public class CustomerOrderRequest
    {
        public int CustomerId { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
