using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.DTOs.OrderDetailDtos
{
    public class GetOrderDetailServiceDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
