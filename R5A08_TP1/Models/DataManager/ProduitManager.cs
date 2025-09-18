using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        readonly ProduitsDbContext? produitsDbContext;

        public ProduitManager() { }

        public ProduitManager(ProduitsDbContext context)
        {
            produitsDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllAsync()
        {
            return await produitsDbContext.Produits.ToListAsync();
        }

        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            return await produitsDbContext.Produits.FirstOrDefaultAsync(p => p.IdProduit == id);
        }
    
        public async Task AddAsync(Produit entity)
        {
            await produitsDbContext.Produits.AddAsync(entity);
            await produitsDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Produit produit, Produit entity)
        {
            produitsDbContext.Produits.Attach(produit);
            produitsDbContext.Entry(produit).CurrentValues.SetValues(entity);
            await produitsDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Produit produit)
        {
            produitsDbContext.Produits.Remove(produit);
            await produitsDbContext.SaveChangesAsync();
        }
    }
}
