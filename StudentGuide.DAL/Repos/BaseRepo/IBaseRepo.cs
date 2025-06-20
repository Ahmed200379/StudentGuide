﻿using Microsoft.EntityFrameworkCore;
using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Repos.BaseRepo
{
   public interface IBaseRepo<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task AddRangeAsync(List<T> entity);
        Task<T?>GetByIdAsync(String id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? expression =null);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<int> TotalCount();
        Task<IEnumerable<T>> GetAllexpressionAsync(
    Expression<Func<T, bool>> filter = null,
    string includeProperties = "");


    }
}
