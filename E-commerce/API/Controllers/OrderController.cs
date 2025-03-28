using E_commerce.Application.Contracts;
using E_commerce.Application.DTOs.OrderHeaderDtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        #region [- Ctor() -]
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region [- Get() -]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetOrderHeaderServiceDto dto)
        {
            Guard_OrderService();
            var getResponse = await _orderService.Get(dto);
            var response = getResponse.Value;
            if (response is null)
            {
                return Json("NotFound");
            }
            return Json(response);
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Guard_OrderService();
            var getAllResponse = await _orderService.GetAll();
            var response = getAllResponse.Value.GetOrderHeaderServiceDtos;
            return Json(response);
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostOrderHeaderServiceDto dto)
        {
            Guard_OrderService();
            var postDto = new GetOrderHeaderServiceDto() { Id = dto.Id };
            var getResponse = await _orderService.Get(postDto);

            switch (ModelState.IsValid)
            {
                case true when getResponse.Value is null:
                    {

                        var postResponse = await _orderService.Post(dto);
                        return postResponse.IsSuccessful ? Ok() : BadRequest();
                    }
                case true when getResponse.Value is not null:
                    return Conflict(dto);
                default:
                    return BadRequest();
            }
        }
        #endregion

        #region [- Put() -]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutOrderHeaderServiceDto dto)
        {
            Guard_OrderService();

            if (ModelState.IsValid)
            {
                var existingProduct = await _orderService.Get(new GetOrderHeaderServiceDto { Id = dto.Id });
                if (existingProduct.Value != null && existingProduct.Value.Id != dto.Id)
                {
                    return Conflict("Email already exists.");
                }
                var putResponse = await _orderService.Put(dto);
                return putResponse.IsSuccessful ? Ok() : BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region [- Delete() -]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderHeaderServiceDto dto)
        {
            Guard_OrderService();
            var deleteResponse = await _orderService.Delete(dto);
            return deleteResponse.IsSuccessful ? Ok() : BadRequest();
        }
        #endregion

        #region [- ProductServiceGuard() -]
        private ObjectResult Guard_OrderService()
        {
            if (_orderService is null)
            {
                return Problem($" {nameof(_orderService)} is null.");
            }

            return null;
        }
        #endregion
    }
}
