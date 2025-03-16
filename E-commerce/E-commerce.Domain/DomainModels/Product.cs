namespace E_commerce.Domain.DomainModels
{
    class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SellerId { get; set; }
    }
}
