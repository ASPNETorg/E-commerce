using E_commerce.Application.DTOs.OrderDetailDtos;

namespace E_commerce.Application.DTOs.OrderHeaderDtos
{
    public class PutOrderHeaderServiceDto
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<PostOrderDetailServiceDto> postOrderDetailServiceDtos { get; set; } = new List<PostOrderDetailServiceDto>();
    }
}
