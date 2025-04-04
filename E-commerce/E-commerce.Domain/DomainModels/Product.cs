﻿using E_commerce.Domain.Common;

namespace E_commerce.Domain.DomainModels
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Stock {get; set; }
        public Guid SellerId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
