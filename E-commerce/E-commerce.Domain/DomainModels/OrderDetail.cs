using E_commerce.Domain.Common;

namespace E_commerce.Domain.DomainModels
{
    public class OrderDetail:BaseEntity
    {
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
