﻿using BlazorApp.Models;

namespace BlazorApp.Services
{
    public interface IService<TEntity>
    {
        Task<IEnumerable<TEntity>?> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity updatedEntity);
        Task DeleteAsync(int id);
        Task<Product?> GetByNameAsync(string name);
    }
}
