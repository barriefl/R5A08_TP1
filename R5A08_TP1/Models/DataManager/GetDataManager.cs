using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class GetDataManager<TEntity> : IGetDataRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext? appDbContext;
        private DbSet<TEntity> dbSet;

        public GetDataManager(AppDbContext context)
        {
            appDbContext = context;
            dbSet = appDbContext.Set<TEntity>();
        }

        public async Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<ActionResult<TEntity>> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
