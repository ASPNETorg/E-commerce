using E_commerce.Application.Contracts.Common;
using E_commerce.ApplicationServices.Dtos.PersonDtos;

namespace E_commerce.ApplicationServices.Contracts;

public interface IPersonService : 
    IService<PostPersonServiceDto, GetPersonServiceDto, GetAllPersonServiceDto, PutPersonServiceDto, DeletePersonServiceDto>
{
}