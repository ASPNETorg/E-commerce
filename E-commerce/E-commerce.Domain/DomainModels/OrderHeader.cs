using E_commerce.Domain.Common;

namespace E_commerce.Domain.DomainModels
{
    public class OrderHeader:BaseEntity
    {
        public Guid BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
