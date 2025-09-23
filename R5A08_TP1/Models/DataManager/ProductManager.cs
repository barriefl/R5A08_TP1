using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProductManager : IDataRepository<Product>
    {
        readonly AppDbContext? appDbContext;

        public ProductManager() { }

        public ProductManager(AppDbContext context)
        {
            appDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            return await appDbContext.Products.ToListAsync();
        }

        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            return await appDbContext.Products.FirstOrDefaultAsync(p => p.IdProduct == id);
        }
    
        public async Task AddAsync(Product entity)
        {
            await appDbContext.Products.AddAsync(entity);
            await appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product produit, Product entity)
        {
            appDbContext.Products.Attach(produit);
            appDbContext.Entry(produit).CurrentValues.SetValues(entity);
            await appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Product produit)
        {
            appDbContext.Products.Remove(produit);
            await appDbContext.SaveChangesAsync();
        }
    }
}
