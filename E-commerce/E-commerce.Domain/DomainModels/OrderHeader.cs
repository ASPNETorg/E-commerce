namespace E_commerce.Domain.DomainModels
{
    class OrderHeader
    {
        public Guid BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
