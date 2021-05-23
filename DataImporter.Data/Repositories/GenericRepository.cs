using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataImporter.Data.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fields

        protected DataImporterDbContext _dbContext;

        #endregion

        public GenericRepository(DataImporterDbContext context)
        {
            _dbContext = context;
        }

        #region Public Methods


        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => _dbContext.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> GetFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync();
        }


        public async Task<T> Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => _dbContext.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => _dbContext.Set<T>().CountAsync(predicate);

        #endregion
    }
}

