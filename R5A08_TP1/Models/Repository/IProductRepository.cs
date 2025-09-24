using Microsoft.AspNetCore.Mvc;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Repository
{
    public interface IProductRepository : IWriteDataRepository<Product>
    {
        Task<ActionResult<IEnumerable<Product>>> GetProductsByNameAsync(string name);
        Task<ActionResult<IEnumerable<Product>>> GetAllWithIncludesAsync();
        Task<ActionResult<Product>> GetByIdWithIncludesAsync(int id);
    }
}
