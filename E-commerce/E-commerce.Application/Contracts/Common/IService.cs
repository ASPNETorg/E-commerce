﻿using E_commerce.Infrastructure.Frameworks.ResponseFrameworks.Contracts;

namespace E_commerce.Application.Contracts.Common
{
    public interface IService<TPost, TGet, TGetAll, TUpdate, TDelete>
    {
        Task<IResponse<TGetAll>> GetAll();
        Task<IResponse<TGet>> Get(TGet dto);
        Task<IResponse<TPost>> Post(TPost dto);
        Task<IResponse<TUpdate>> Put(TUpdate dto);
        Task<IResponse<TDelete>> Delete(TDelete dto);
    }
}
