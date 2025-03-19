using E_commerce.Application.Contracts.Common;
using E_commerce.ApplicationServices.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Contracts
{
    public interface IProductService :
    IService<PostProductServiceDto, GetProductServiceDto, GetAllProductServiceDto, PutProductServiceDto, DeleteProductServiceDto>
    {

    }
}
