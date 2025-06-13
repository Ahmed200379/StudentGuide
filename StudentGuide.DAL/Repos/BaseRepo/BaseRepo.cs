using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.BaseRepo
{
  public  class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public async Task<IEnumerable<T>> GetAllexpressionAsync(
    Expression<Func<T, bool>> filter = null,
    string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return await query.ToListAsync();
        }

        public async Task Delete(T entity)
        {
              _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task AddRangeAsync(List<T> entity)
        {
            await _context.Set<T>().AddRangeAsync(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression =null)
        {
            IQueryable<T> allData = _context.Set<T>();
            if(expression != null)
            {
                allData = allData.Where(expression);
            }
            return await allData.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
        public async Task<int> TotalCount()
        {
            return await _context.Set<T>().CountAsync();
        }
    }
}
