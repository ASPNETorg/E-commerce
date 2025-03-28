using E_commerce.Domain.DomainModels;
using E_commerce.Infrastructure.Contracts;

namespace E_commerce.Infrastructure.Models.Services.Contracts
{
    public interface IPersonRepository: IRepository<Person, IEnumerable<Person>>
    {
       Task<Person> SelectByEmailAsync(string email);
    }
}
