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
            // produitsDbContext.Produits.Attach(entityToUpdate);
            // produitsDbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            // await context.SaveChangesAsync();
            produitsDbContext.Entry(produit).State = EntityState.Modified;
            produit.IdProduit = entity.IdProduit;
            produit.NomProduit = entity.NomProduit;
            produit.DescriptionProduit = entity.DescriptionProduit;
            produit.NomPhotoProduit = entity.NomPhotoProduit;
            produit.UriPhotoProduit= entity.UriPhotoProduit;
            produit.StockReelProduit = entity.StockReelProduit;
            produit.StockMinProduit = entity.StockMinProduit;
            produit.StockMaxProduit = entity.StockMaxProduit;
            produit.TypeProduitNavigation = entity.TypeProduitNavigation;
            produit.MarqueNavigation = entity.MarqueNavigation;
            await produitsDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Produit produit)
        {
            produitsDbContext.Produits.Remove(produit);
            await produitsDbContext.SaveChangesAsync();
        }
    }
}
