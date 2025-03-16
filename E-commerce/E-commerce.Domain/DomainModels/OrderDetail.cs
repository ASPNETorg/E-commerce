namespace E_commerce.Domain.DomainModels
{
    class OrderDetail
    {
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
