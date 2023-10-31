using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TimeSeriesApi.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>> predicate);
        //Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
