using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public  async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
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
    }
}
