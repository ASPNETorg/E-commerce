using E_commerce.Domain.DomainModels;
using E_commerce.Infrastructure.Contracts;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;

namespace E_commerce.Infrastructure.Models.Services.Contracts
{
    public interface IOrderRepository: IRepository<OrderHeader, IEnumerable<OrderHeader>>
    {
        Task<IResponse<OrderHeader>> SelectOrderByUserAsync(Guid id);
    }
}
