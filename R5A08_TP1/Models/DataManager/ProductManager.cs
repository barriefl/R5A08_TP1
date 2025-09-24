using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProductManager : WriteDataManager<Product>, IProductRepository
    {
        private readonly AppDbContext? appDbContext;
        private readonly DbSet<Product> products;

        public ProductManager(AppDbContext context) : base(context)
        {
            appDbContext = context;
            products = appDbContext.Products;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByNameAsync(string name)
        {
            return await products
                .Include(p => p.BrandNavigation)
                .Include(p => p.ProductTypeNavigation)
                .Where(p => p.NameProduct.ToUpper() == name.ToUpper()).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllWithIncludesAsync()
        {
            return await products
                .Include(p => p.BrandNavigation)
                .Include(p => p.ProductTypeNavigation)
                .ToListAsync();
        }

        public async Task<ActionResult<Product>> GetByIdWithIncludesAsync(int id)
        {
            return await products
                .Include(p => p.BrandNavigation)
                .Include(p => p.ProductTypeNavigation)
                .FirstOrDefaultAsync(p => p.IdProduct == id);
        }
    }
}
