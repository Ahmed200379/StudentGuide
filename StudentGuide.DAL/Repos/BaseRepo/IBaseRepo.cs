using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.BaseRepo
{
   public interface IBaseRepo<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        
    }
}
