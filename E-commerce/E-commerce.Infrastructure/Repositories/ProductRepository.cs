using E_commerce.Domain.DomainModels;
using E_commerce.Infrastructure.Data;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;
using E_commerce.Infrastructure.Frameworks;
using E_commerce.Infrastructure.Models.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks;

namespace E_commerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProjectDbContext _dbContext;
        #region [- Ctor -]
        public ProductRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region [- Insert() -]
        public async Task<IResponse<Product>> InsertAsync(Product model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _dbContext.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                var response = new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- SelectAll() -]
        public async Task<IResponse<IEnumerable<Product>>> SelectAllAsync()
        {
            try
            {
                var product = await _dbContext.Products.AsNoTracking().ToListAsync();
                return product is null ?
                    new Response<IEnumerable<Product>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<IEnumerable<Product>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, product);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- Select() -]
        public async Task<IResponse<Product>> SelectByIdAsync(Product model)
        {
            try
            {
                var responseValue = new Product();
                if (model.Id.ToString() != "")
                {
                    responseValue = await _dbContext.Products.Where(c => c.Name == model.Name).SingleOrDefaultAsync();
                }
                else
                {
                    responseValue = await _dbContext.Products.FindAsync(model.Id);
                }
                return responseValue is null ?
                     new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                     new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- Update() -]
        public async Task<IResponse<Product>> UpdateAsync(Product model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                //_projectDbContext.Update(model);
                _dbContext.Entry(model).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var response = new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- Delete() -]
        public async Task<IResponse<Product>> DeleteAsync(Guid id)
        {
            try
            {
                var DeleteRecord = await _dbContext.Products.FindAsync(id);
                if (DeleteRecord == null)
                {
                    return new Response<Product>(false, HttpStatusCode.NotFound, "Person not found", null);

                }
                if (DeleteRecord is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _dbContext.Products.Remove(DeleteRecord);
                await _dbContext.SaveChangesAsync();
                var response = new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, DeleteRecord);
                return response;
            }
            catch (Exception)
            {
                return new Response<Product>(false, HttpStatusCode.InternalServerError, "Message", null);
            }
        }
        #endregion

        #region [- Search() -]
        public async Task<IResponse<Product>> SearchAsync(string searchTerm)
        {
            var response = new Product();
            try
            {
                if (searchTerm is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _dbContext.Products.Where(p => p.Name.Contains(searchTerm)
                || p.Description.Contains(searchTerm)).ToListAsync();
                return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, response);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
