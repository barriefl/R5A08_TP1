using Microsoft.AspNetCore.Mvc;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Repository
{
    public interface IBrandRepository : IWriteDataRepository<Brand>
    {
        Task<ActionResult<Brand>> GetBrandByNameAsync(string name);
    }
}
