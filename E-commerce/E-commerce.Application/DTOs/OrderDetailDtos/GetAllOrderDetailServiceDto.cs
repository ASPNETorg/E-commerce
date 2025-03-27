using E_commerce.ApplicationServices.Dtos.PersonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.DTOs.OrderDetailDtos
{
    public class GetAllOrderDetailServiceDto
    {
        public List<GetOrderDetailServiceDto> GetOrderDetailServiceDtos { get; set; }
    }
}
