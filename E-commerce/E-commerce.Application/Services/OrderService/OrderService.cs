using E_commerce.Application.Contracts;
using E_commerce.Application.DTOs.OrderHeaderDtos;
using E_commerce.ApplicationServices.Dtos.PersonDtos;
using E_commerce.Domain.DomainModels;
using E_commerce.Infrastructure.Frameworks;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;
using E_commerce.Infrastructure.Models.Services.Contracts;
using System.Net;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks;

namespace E_commerce.Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPersonRepository _personRepository;

        #region [- Ctor() -]
        public OrderService(IOrderRepository orderRepository, IPersonRepository personRepository)
        {
            _orderRepository = orderRepository;
            _personRepository = personRepository;
        }
        #endregion

        #region [- Delete() -]
        public async Task<IResponse<DeleteOrderHeaderServiceDto>> Delete(DeleteOrderHeaderServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<DeleteOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var deleteResponse = await _orderRepository.DeleteAsync(dto.Id);

            if (deleteResponse is null || !deleteResponse.IsSuccessful)
            {
                return new Response<DeleteOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            var response = new Response<DeleteOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
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
        public async Task<IResponse<PostOrderHeaderServiceDto>> Post(PostOrderHeaderServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PostOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var order = new OrderHeader()
            {
                Id = Guid.NewGuid(),
                BuyerId = Guid.NewGuid(),
                TotalAmount = dto.TotalAmount,
                OrderDate = dto.OrderDate,
                OrderDetails = dto.postOrderDetailServiceDtos.Select(od => new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                }).ToList(),
            };
            var insertResponse = await _orderRepository.InsertAsync(order);

            if (!insertResponse.IsSuccessful)
            {
                return new Response<PostOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PostOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

        #region [- Put() -]
        public async Task<IResponse<PutOrderHeaderServiceDto>> Put(PutOrderHeaderServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PutOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            // Retrieve the existing order by ID
            var existingOrderResponse = await _orderRepository.SelectOrderByUserAsync(dto.Id);
            if (existingOrderResponse == null || existingOrderResponse.Value == null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            var existingOrder = existingOrderResponse.Value;
           
            existingOrder.BuyerId = dto.BuyerId;
            existingOrder.TotalAmount = dto.TotalAmount; 
                                                         
            if (dto.postOrderDetailServiceDtos != null && dto.postOrderDetailServiceDtos.Any())
            {
                existingOrder.OrderDetails = dto.postOrderDetailServiceDtos.Select(od => new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList();
            }
            await _orderRepository.UpdateAsync(existingOrder);
            
            return new Response<PutOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
        }
        #endregion

    }
}
