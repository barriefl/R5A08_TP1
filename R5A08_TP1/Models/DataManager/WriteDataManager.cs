using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class WriteDataManager<TEntity> : IWriteDataRepository<TEntity> where TEntity : class
    {
        readonly AppDbContext? appDbContext;
        private DbSet<TEntity> dbSet;

        public WriteDataManager(AppDbContext context)
        {
            appDbContext = context;
            dbSet = appDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entityToUpdate, TEntity newEntity)
        {
            dbSet.Attach(entityToUpdate);
            appDbContext.Entry(entityToUpdate).CurrentValues.SetValues(newEntity);
            await appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            await appDbContext.SaveChangesAsync();
        }
    }
}
