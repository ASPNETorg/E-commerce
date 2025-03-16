using E_commerce.Domain.DomainModels;
using E_commerce.Infrastructure.Data;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;
using E_commerce.Infrastructure.Frameworks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using E_commerce.Infrastructure.Frameworks.ResponseFrameworks;
using E_commerce.Infrastructure.Models.Services.Contracts;

namespace E_commerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProjectDbContext _dbContext;
        #region [- Ctor -]
        public OrderRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region [- Insert() -]
        public async Task<IResponse<OrderHeader>> InsertAsync(OrderHeader model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _dbContext.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                var response = new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- SelectAll() -]
        public async Task<IResponse<IEnumerable<OrderHeader>>> SelectAllAsync()
        {
            try
            {
                var person = await _dbContext.OrderHeaders.AsNoTracking().ToListAsync();
                return person is null ?
                    new Response<IEnumerable<OrderHeader>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<IEnumerable<OrderHeader>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, person);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

         #region [- SelectOrderByUser -]
        public async Task<IResponse<OrderHeader>> SelectOrderByUserAsync(Guid id)
        {
            var response = new OrderHeader();
            try
            {
                if(_dbContext.OrderHeaders == null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _dbContext.OrderHeaders.Where(c => c.BuyerId == id).ToListAsync();
                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation,response);
            }
            catch (Exception)
            {
                throw;
            }
            
        } 
        #endregion

        #region [- Select() -]
        public async Task<IResponse<OrderHeader>> SelectByIdAsync(OrderHeader model)
        {
            try
            {
                var responseValue = new OrderHeader();
                if (model.Id.ToString() != "")
                {
                    //responseValue = await _projectDbContext.Person.FindAsync(person.Email);
                    responseValue = await _dbContext.OrderHeaders.Where(c => c.Id == model.Id).SingleOrDefaultAsync();
                }
                else
                {
                    responseValue = await _dbContext.OrderHeaders.FindAsync(model.Id);
                }
                return responseValue is null ?
                     new Response<OrderHeader   >(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                     new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- Update() -]
        public async Task<IResponse<OrderHeader>> UpdateAsync(OrderHeader model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                //_projectDbContext.Update(model);
                _dbContext.Entry(model).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var response = new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- Delete() -]
        public async Task<IResponse<OrderHeader>> DeleteAsync(Guid id)
        {
            try
            {
                var DeleteRecord = await _dbContext.OrderHeaders.FindAsync(id);
                if (DeleteRecord == null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.NotFound, "Person not found", null);

                }
                if (DeleteRecord is null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _dbContext.OrderHeaders.Remove(DeleteRecord);
                await _dbContext.SaveChangesAsync();
                var response = new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, DeleteRecord);
                return response;
            }
            catch (Exception)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError, "Message", null);
            }
        }
        #endregion
    }
}
