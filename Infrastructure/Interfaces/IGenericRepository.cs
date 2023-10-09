using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnniesTech.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        Task<T> GetByIdAsync(int id);
            Task<IEnumerable<T>> GetAllAsync();
             Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
            Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
            void Add(T entity);
            void AddRange(IEnumerable<T> entities);
            void Remove(T entity);
            void RemoveRange(IEnumerable<T> entities);
            void Update(T entity);
        
    }
}