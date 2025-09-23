using Microsoft.AspNetCore.Mvc;

namespace R5A08_TP1.Models.Repository
{
    public interface IGetDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
    }
}
