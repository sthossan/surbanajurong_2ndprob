using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Models.TimeSeriesApi;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace TimeSeriesApi.Repositories
{
    // Repositories/Repository.cs
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CustomDbContext _context;

        public GenericRepository(CustomDbContext context) => _context = context;

        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking().Where(predicate);
            return query;
        }

        public virtual IQueryable<T> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().Where(predicate);
        }

        //public virtual async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        //}

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
