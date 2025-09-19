using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProduitManager : IDataRepository<Product>
    {
        readonly AppDbContext? produitsDbContext;

        public ProduitManager() { }

        public ProduitManager(AppDbContext context)
        {
            produitsDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            return await produitsDbContext.Products.ToListAsync();
        }

        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            return await produitsDbContext.Products.FirstOrDefaultAsync(p => p.IdProduit == id);
        }
    
        public async Task AddAsync(Product entity)
        {
            await produitsDbContext.Products.AddAsync(entity);
            await produitsDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product produit, Product entity)
        {
            produitsDbContext.Products.Attach(produit);
            produitsDbContext.Entry(produit).CurrentValues.SetValues(entity);
            await produitsDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Product produit)
        {
            produitsDbContext.Products.Remove(produit);
            await produitsDbContext.SaveChangesAsync();
        }
    }
}
