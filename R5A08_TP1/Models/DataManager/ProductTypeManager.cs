using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProductTypeManager : WriteDataManager<ProductType>, IProductTypeRepository
    {
        private readonly AppDbContext? appDbContext;
        private readonly DbSet<ProductType> productTypes;

        public ProductTypeManager(AppDbContext context) : base(context)
        {
            appDbContext = context;
            productTypes = appDbContext.ProductTypes;
        }

        public async Task<ActionResult<ProductType>> GetProductTypeByNameAsync(string name)
        {
            return await productTypes.FirstOrDefaultAsync(p => p.NameProductType.ToUpper() == name.ToUpper());
        }
    }
}
