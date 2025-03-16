using E_commerce.Domain.Common;

namespace E_commerce.Domain.DomainModels
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SellerId { get; set; }
    }
}
