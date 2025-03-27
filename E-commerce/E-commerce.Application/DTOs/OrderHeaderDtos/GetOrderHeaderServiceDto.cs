using E_commerce.Application.DTOs.OrderDetailDtos;

namespace E_commerce.Application.DTOs.OrderHeaderDtos
{
    public class GetOrderHeaderServiceDto
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public List<GetOrderDetailServiceDto> getOrderDetailServiceDtos {get; set;} = new List<GetOrderDetailServiceDto>();
    }
}
