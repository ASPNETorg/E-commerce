using E_commerce.ApplicationServices.Contracts;
using E_commerce.ApplicationServices.Dtos.PersonDtos;
using E_commerce.Infrastructure.Models.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IPersonRepository _personRepository;

        #region [- Ctor -]
        public PersonController(IPersonService personService, IPersonRepository personRepository)
        {
            _personService = personService;
            _personRepository = personRepository;
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
            var existingUser = await _personRepository.SelectByEmailAsync(dto.Email);

            if (dto.Role == "Buyer")
            {
                if (existingUser != null)
                {
                    return Conflict("A buyer with this email already exists.");
                }
            }
            else if (dto.Role == "Seller")
            {
                if (existingUser == null)
                {
                    return NotFound("Seller not found. Please sign up first.");
                }
            }
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

            var putDto = new GetPersonServiceDto() { Email = dto.Email };

            #region [- For checking & avoiding email duplication -]
            //var getResponse = await _personService.Get(putDto);//For checking & avoiding email duplication
            //switch (ModelState.IsValid)
            //{
            //    case true when getResponse.Value is null:
            //    {
            //        var putResponse = await _personService.Put(dto);
            //        return putResponse.IsSuccessful ? Ok() : BadRequest();
            //    }
            //    case true when getResponse.Value is not null://For checking & avoiding email duplication
            //        return Conflict(dto);
            //    default:
            //        return BadRequest();
            //} 
            #endregion

            if (ModelState.IsValid)
            {
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
