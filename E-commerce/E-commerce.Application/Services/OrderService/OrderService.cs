using E_commerce.Application.Contracts;
using E_commerce.Application.DTOs.OrderHeaderDtos;
using E_commerce.ApplicationServices.Dtos.PersonDtos;
using E_commerce.Domain.DomainModels;
using E_commerce.Infrastructure.Frameworks;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;
using E_commerce.Infrastructure.Models.Services.Contracts;
using System.Net;
using System;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks;

namespace E_commerce.Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPersonRepository _personRepository;

        public OrderService(IOrderRepository orderRepository, IPersonRepository personRepository)
        {
            _orderRepository = orderRepository;
            _personRepository = personRepository;
        }

        #region [- Delete() -]
        public Task<IResponse<DTOs.OrderHeaderDtos.DeleteOrderHeaderServiceDto>> Delete(DTOs.OrderHeaderDtos.DeleteOrderHeaderServiceDto dto)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region [- Get() -]
        public async Task<IResponse<GetOrderHeaderServiceDto>> Get(GetOrderHeaderServiceDto dto)
        {
            var order = new OrderHeader()
            {
                Id = dto.Id,
                BuyerId = dto.BuyerId,
                TotalAmount = dto.TotalAmount,
                OrderDate = dto.OrderDate,
            };
            var selectResponse = await _orderRepository.SelectByIdAsync(order);

            if (selectResponse is null)
            {
                return new Response<GetOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectResponse.IsSuccessful)
            {
                return new Response<GetOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }
            var getOrderHeaderServiceDto = new GetOrderHeaderServiceDto()
            {
                Id = selectResponse.Value.Id,
                BuyerId = selectResponse.Value.BuyerId,
                TotalAmount = selectResponse.Value.TotalAmount,
                OrderDate = selectResponse.Value.OrderDate
            };
            var response = new Response<GetOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getOrderHeaderServiceDto);
            return response;
        }
        #endregion

        #region [- GetAll() -]
        public async Task<IResponse<GetAllOrderHeaderServiceDto>> GetAll()
        {
            var selectAllResponse = await _orderRepository.SelectAllAsync();

            if (selectAllResponse is null)
            {
                return new Response<GetAllOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectAllResponse.IsSuccessful)
            {
                return new Response<GetAllOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }

            var getAllOrderDto = new GetAllOrderHeaderServiceDto() { GetOrderHeaderServiceDtos = new List<GetOrderHeaderServiceDto>() };

            foreach (var item in selectAllResponse.Value)
            {
                var orderDto = new GetOrderHeaderServiceDto()
                {
                    Id = (Guid)item.Id,
                    BuyerId = item.BuyerId,
                    TotalAmount = item.TotalAmount,
                    OrderDate = item.OrderDate
                };
                getAllOrderDto.GetOrderHeaderServiceDtos.Add(orderDto);
            }

            var response = new Response<GetAllOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getAllOrderDto);
            return response;
        }
        #endregion

        #region [- Post() -]
        public Task<IResponse<PostOrderHeaderServiceDto>> Post(PostOrderHeaderServiceDto dto)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- Put() -]
        public Task<IResponse<DTOs.OrderHeaderDtos.PutOrderHeaderServiceDto>> Put(DTOs.OrderHeaderDtos.PutOrderHeaderServiceDto dto)
        {
            throw new NotImplementedException();
        } 
        #endregion

    }
}
