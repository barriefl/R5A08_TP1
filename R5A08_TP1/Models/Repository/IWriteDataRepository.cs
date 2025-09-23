using Microsoft.AspNetCore.Mvc;

namespace R5A08_TP1.Models.Repository
{
    public interface IWriteDataRepository<TEntity> : IGetDataRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
