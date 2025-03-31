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
    public class PersonRepository : IPersonRepository
    {
        private readonly ProjectDbContext _dbContext;
        #region [- Ctor -]
        public PersonRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region [- Insert() -]
        public async Task<IResponse<Person>> InsertAsync(Person model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _dbContext.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                var response = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- SelectAll() -]
        public async Task<IResponse<IEnumerable<Person>>> SelectAllAsync()
        {
            try
            {
                var person = await _dbContext.People.AsNoTracking().ToListAsync();
                return person is null ?
                    new Response<IEnumerable<Person>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<IEnumerable<Person>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, person);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- Select() -]
        public async Task<IResponse<Person>> SelectByIdAsync(Person model)
        {
            try
            {
                var responseValue = new Person();
                if (model.Id.ToString() != "")
                {
                    //responseValue = await _projectDbContext.Person.FindAsync(person.Email);
                    responseValue = await _dbContext.People.Where(c => c.Email == model.Email).SingleOrDefaultAsync();
                }
                else
                {
                    responseValue = await _dbContext.People.FindAsync(model.Id);
                }
                return responseValue is null ?
                     new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                     new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- SelectByEmail() -]
        public async Task<Person> SelectByEmailAsync(string email)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.Email == email);
        }
        #endregion

        #region [- Update() -]
        public async Task<IResponse<Person>> UpdateAsync(Person model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _dbContext.Update(model);
                //_dbContext.Entry(model).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var response = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        //#region [- Update() -]
        //public async Task<IResponse<Person>> UpdateAsync(Person model)
        //{
        //    if (model is null)
        //    {
        //        return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
        //    }

        //    try
        //    {
        //        // Attach the entity to the context
        //        _dbContext.Entry(model).State = EntityState.Modified;

        //        // Attempt to save changes
        //        await _dbContext.SaveChangesAsync();

        //        // Return success response
        //        return new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        // Handle concurrency exception
        //        foreach (var entry in ex.Entries)
        //        {
        //            if (entry.Entity is Person)
        //            {
        //                var proposedValues = entry.CurrentValues;
        //                var databaseValues = await entry.GetDatabaseValuesAsync();

        //                // Optionally, you can log the values for debugging
        //                // Log the proposed and database values if needed

        //                // Decide how to handle the conflict
        //                // For example, you can keep the database values or merge them
        //                // Here, we will just return a conflict response
        //                return new Response<Person>(false, HttpStatusCode.Conflict, ResponseMessages.Error, null);
        //            }
        //            else
        //            {
        //                throw new NotSupportedException(
        //                    "Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
        //            }
        //        }
        //        throw; // Rethrow if not handled
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (optional)
        //        // LogError(ex);
        //        return new Response<Person>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
        //    }
        //}
        //#endregion

        #region [- Delete() -]
        public async Task<IResponse<Person>> DeleteAsync(Guid id)
        {
            try
            {
                var DeleteRecord = await _dbContext.People.FindAsync(id);
                if (DeleteRecord == null)
                {
                    return new Response<Person>(false, HttpStatusCode.NotFound, "Person not found", null);

                }
                if (DeleteRecord is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _dbContext.People.Remove(DeleteRecord);
                await _dbContext.SaveChangesAsync();
                var response = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, DeleteRecord);
                return response;
            }
            catch (Exception)
            {
                return new Response<Person>(false, HttpStatusCode.InternalServerError, "Message", null);
            }
        }


        #endregion
    }
}
