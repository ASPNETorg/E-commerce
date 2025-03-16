using E_commerce.Domain.Common;

namespace E_commerce.Domain.DomainModels
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
