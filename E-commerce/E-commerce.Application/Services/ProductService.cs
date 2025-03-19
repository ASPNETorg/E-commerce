using E_commerce.Application.Contracts;
using E_commerce.ApplicationServices.Dtos.ProductDtos;
using E_commerce.Infrastructure.Frameworks;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;
using E_commerce.Infrastructure.Models.Services.Contracts;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks;
using System.Net;
using E_commerce.Domain.DomainModels;

namespace E_commerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;

        #region [- Ctor -]
        public ProductService(IProductRepository productRepository, IPersonRepository personRepository)
        {
            _productRepository = productRepository;
             _personRepository = personRepository;
        }
        #endregion

        #region [- Delete() -]
        public async Task<IResponse<DeleteProductServiceDto>> Delete(DeleteProductServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<DeleteProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var deleteResponse = await _productRepository.DeleteAsync(dto.Id);

            if (deleteResponse is null || !deleteResponse.IsSuccessful)
            {
                return new Response<DeleteProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            var response = new Response<DeleteProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

        #region [- GetById() -]
        public Task<IResponse<GetProductServiceDto>> Get(GetProductServiceDto dto)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- GetAll() -]
        public async Task<IResponse<GetAllProductServiceDto>> GetAll()
        {
            var selectAllResponse = await _productRepository.SelectAllAsync();

            if (selectAllResponse is null)
            {
                return new Response<GetAllProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectAllResponse.IsSuccessful)
            {
                return new Response<GetAllProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }

            var getAllProductDto = new GetAllProductServiceDto() { GetProductServiceDtos = new List<GetProductServiceDto>() };

            foreach (var item in selectAllResponse.Value)
            {
                var productDto = new GetProductServiceDto()
                {
                    Id = (Guid)item.Id,
                    Name = item.Name,
                    Price = item.Price
                };
                getAllProductDto.GetProductServiceDtos.Add(productDto);
            }
            var response = new Response<GetAllProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getAllProductDto);
            return response;
        }
        #endregion

        #region [- Post() -]
        public async Task<IResponse<PostProductServiceDto>> Post(PostProductServiceDto dto, Guid id)
        {
            if (dto is null)
            {
                return new Response<PostProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var person = await _personRepository.SelectByIdAsync(id);
            if(person == null || person.Role != "Seller")
            {
                throw new UnauthorizedAccessException("Only Sellers can add products.");
            }
            var postProduct = new Product()
            {
                Id = new Guid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                SellerId = dto.SellerId
            };
            var insertResponse = await _productRepository.InsertAsync(postProduct);

            if (!insertResponse.IsSuccessful)
            {
                return new Response<PostProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PostProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

        #region [- Put() -]
        public async Task<IResponse<PutProductServiceDto>> Put(PutProductServiceDto dto, Guid id)
        {
            if (dto is null)
            {
                return new Response<PutProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var person = await _personRepository.SelectByIdAsync(id);
            if(person == null || person.Role != "Seller")
            {
                throw new UnauthorizedAccessException("Only Sellers can update products.");
            }
            var putProduct = new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                SellerId = dto.SellerId,
            };
            var updateResponse = await _productRepository.UpdateAsync(putProduct);

            if (!updateResponse.IsSuccessful)
            {
                return new Response<PutProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PutProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion
    }
}
