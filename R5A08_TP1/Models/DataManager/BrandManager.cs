using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class BrandManager : WriteDataManager<Brand>, IBrandRepository
    {
        private readonly AppDbContext? appDbContext;
        private readonly DbSet<Brand> brands;

        public BrandManager(AppDbContext context) : base(context)
        {
            appDbContext = context;
            brands = appDbContext.Brands;
        }

        public async Task<ActionResult<Brand>> GetBrandByNameAsync(string name)
        {
            return await brands.FirstOrDefaultAsync(b => b.NameBrand.ToUpper() == name.ToUpper());
        }
    }
}
