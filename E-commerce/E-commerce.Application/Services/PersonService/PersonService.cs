
using E_commerce.ApplicationServices.Dtos.PersonDtos;
using E_commerce.Infrastructure.Frameworks;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;
using System.Net;
using E_commerce.ApplicationServices.Contracts;
using E_commerce.Infrastructure.Models.Services.Contracts;
using E_commerce.Domain.DomainModels;

namespace E_commerce.Application.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        #region [- ctor -]
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion

        #region [- GetAll() -]
        public async Task<IResponse<GetAllPersonServiceDto>> GetAll()
        {
             var selectAllResponse = await _personRepository.SelectAllAsync();

            if (selectAllResponse is null)
            {
                return new Response<GetAllPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectAllResponse.IsSuccessful)
            {
                return new Response<GetAllPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }

            var getAllPersonDto = new GetAllPersonServiceDto() { GetPersonServiceDtos = new List<GetPersonServiceDto>() };

            foreach (var item in selectAllResponse.Value)
            {
                var personDto = new GetPersonServiceDto()
                {
                    Id = (Guid)item.Id,
                    FirstName = item.FName,
                    LastName = item.LName,
                    Email = item.Email
                };
                getAllPersonDto.GetPersonServiceDtos.Add(personDto);
            }

            var response = new Response<GetAllPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getAllPersonDto);
            return response;
        }
        #endregion

        #region [- Get() -]
        public async Task<IResponse<GetPersonServiceDto>> Get(GetPersonServiceDto dto)
        {
            var person = new Person()
            {
                Id = dto.Id,
                FName = dto.FirstName,
                LName = dto.LastName,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                Role = dto.Role,
            };
            var selectResponse = await _personRepository.SelectByIdAsync(person);

            if (selectResponse is null)
            {
                return new Response<GetPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectResponse.IsSuccessful)
            {
                return new Response<GetPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }
            var getPersonServiceDto = new GetPersonServiceDto()
            {
                Id = selectResponse.Value.Id,
                FirstName = selectResponse.Value.FName,
                LastName = selectResponse.Value.LName,
                Email = selectResponse.Value.Email,
                PasswordHash=selectResponse.Value.PasswordHash,
                Role = selectResponse.Value.Role,
            };
            var response = new Response<GetPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getPersonServiceDto);
            return response;
        }
        #endregion


        #region [- GetByEmail() -]
        public async Task<bool> IsEmailDublicated(string email)
        {
            var existingPerson = await _personRepository.GetByEmailAsync(email);
            return existingPerson != null;
        } 
        #endregion

        #region [- Post() -]
        public async Task<IResponse<PostPersonServiceDto>> Post(PostPersonServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PostPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var postPerson = new Person()
            {
                Id = new Guid(),
                FName = dto.FirstName,
                LName = dto.LastName,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                Role = dto.Role,
            };
            var insertResponse = await _personRepository.InsertAsync(postPerson);

            if (!insertResponse.IsSuccessful)
            {
                return new Response<PostPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PostPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

        #region [- Put() -]
        public async Task<IResponse<PutPersonServiceDto>> Put(PutPersonServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PutPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var putPerson = new Person()
            {
                Id = dto.Id,
                FName = dto.FirstName,
                LName = dto.LastName,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                Role = dto.Role,
            };
            var updateResponse = await _personRepository.UpdateAsync(putPerson);

            if (!updateResponse.IsSuccessful)
            {
                return new Response<PutPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PutPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

        #region [- Delete() -]
        public async Task<IResponse<DeletePersonServiceDto>> Delete(DeletePersonServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<DeletePersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var deleteResponse = await _personRepository.DeleteAsync(dto.Id);

            if (deleteResponse is null || !deleteResponse.IsSuccessful)
            {
                return new Response<DeletePersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            var response = new Response<DeletePersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

    }
}
