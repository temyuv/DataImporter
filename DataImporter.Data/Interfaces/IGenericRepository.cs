using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataImporter.Data.Repositories
{
    public interface IGenericRepository<T>
    {
        // Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> Add(T entity);


        Task Update(T entity);
        Task Remove(T entity);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}
