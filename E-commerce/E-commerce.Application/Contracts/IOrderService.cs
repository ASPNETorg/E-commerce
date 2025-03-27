using E_commerce.Application.Contracts.Common;
using E_commerce.Application.DTOs.OrderHeaderDtos;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Contracts
{
    public interface IOrderService : IService<PostOrderHeaderServiceDto, GetOrderHeaderServiceDto, GetAllOrderHeaderServiceDto, PutOrderHeaderServiceDto, DeleteOrderHeaderServiceDto>
    {
        
    }
}
