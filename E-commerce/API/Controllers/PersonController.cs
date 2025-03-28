using E_commerce.ApplicationServices.Contracts;
using E_commerce.ApplicationServices.Dtos.PersonDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controllers]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        #region [- Ctor -]
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        #endregion

        #region [- GetById -]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetPersonServiceDto dto)
        {
            Guard_PersonService();
            var getResponse = await _personService.Get(dto);
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
            Guard_PersonService();
            var getAllResponse = await _personService.GetAll();
            var response = getAllResponse.Value.GetPersonServiceDtos;
            return Json(response);
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostPersonServiceDto dto)
        {
            Guard_PersonService();
            var postDto = new GetPersonServiceDto() { Email = dto.Email };
            var getResponse = await _personService.Get(postDto);

            switch (ModelState.IsValid)
            {
                case true when getResponse.Value is null:
                    {

                        var postResponse = await _personService.Post(dto);
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
        public async Task<IActionResult> Put([FromBody] PutPersonServiceDto dto)
        {
            Guard_PersonService();

            if (ModelState.IsValid)
            {
                var existingPerson = await _personService.Get(new GetPersonServiceDto {Email = dto.Email});
                if (existingPerson.Value != null && existingPerson.Value.Id != dto.Id)
                {
                    return Conflict("Email already exists.");
                }
                var putResponse = await _personService.Put(dto);
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
        public async Task<IActionResult> Delete([FromBody] DeletePersonServiceDto dto)
        {
            Guard_PersonService();
            var deleteResponse = await _personService.Delete(dto);
            return deleteResponse.IsSuccessful ? Ok() : BadRequest();
        }
        #endregion

        #region [- PersonServiceGuard() -]
        private ObjectResult Guard_PersonService()
        {
            if (_personService is null)
            {
                return Problem($" {nameof(_personService)} is null.");
            }

            return null;
        }
        #endregion
    }
}
