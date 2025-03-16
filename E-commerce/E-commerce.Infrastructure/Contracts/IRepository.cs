using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;

namespace E_commerce.Infrastructure.Contracts
{
    public interface IRepository<T, TCollection>
    {
        Task<IResponse<TCollection>> SelectAllAsync();
        Task<IResponse<T>> SelectByIdAsync(T obj);
        Task<IResponse<T>> InsertAsync(T obj);
        Task<IResponse<T>> UpdateAsync(T obj);
        Task<IResponse<T>> DeleteAsync(Guid id);
    }
}
