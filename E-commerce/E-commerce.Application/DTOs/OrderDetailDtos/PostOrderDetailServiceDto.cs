namespace E_commerce.Application.DTOs.OrderDetailDtos
{
    public class PostOrderDetailServiceDto
    {
        public Guid Id { get; set; }
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
