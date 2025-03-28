using E_commerce.Application.Contracts;
using E_commerce.ApplicationServices.Dtos.PersonDtos;
using E_commerce.ApplicationServices.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controllers]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        #region [- Ctor() -]
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region [- Get() -]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetProductServiceDto dto)
        {
            Guard_ProductService();
            var getResponse = await _productService.Get(dto);
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
            Guard_ProductService();
            var getAllResponse = await _productService.GetAll();
            var response = getAllResponse.Value.GetProductServiceDtos;
            return Json(response);
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostProductServiceDto dto)
        {
            Guard_ProductService();
            var postDto = new GetProductServiceDto() {Id = dto.CategoryId };
            var getResponse = await _productService.Get(postDto);

            switch (ModelState.IsValid)
            {
                case true when getResponse.Value is null:
                    {

                        var postResponse = await _productService.Post(dto);
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
        public async Task<IActionResult> Put([FromBody] PutProductServiceDto dto)
        {
            Guard_ProductService();

            if (ModelState.IsValid)
            {
                var existingProduct = await _productService.Get(new GetProductServiceDto {Id = dto.CategoryId});
                if (existingProduct.Value != null && existingProduct.Value.Id != dto.Id)
                {
                    return Conflict("Email already exists.");
                }
                var putResponse = await _productService.Put(dto);
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
        public async Task<IActionResult> Delete([FromBody] DeleteProductServiceDto dto)
        {
            Guard_ProductService();
            var deleteResponse = await _productService.Delete(dto);
            return deleteResponse.IsSuccessful ? Ok() : BadRequest();
        }
        #endregion

        #region [- ProductServiceGuard() -]
        private ObjectResult Guard_ProductService()
        {
            if (_productService is null)
            {
                return Problem($" {nameof(_productService)} is null.");
            }

            return null;
        }
        #endregion
    }
}
