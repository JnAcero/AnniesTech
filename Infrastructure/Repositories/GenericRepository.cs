using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Models;
using Microsoft.EntityFrameworkCore;

namespace AnniesTech.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected AnnisTechDbContext _context { get; }
        public GenericRepository(AnnisTechDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Add(entity); 
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> expression)
        {
             return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T> FindFirst(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
           var totalRegistros = await _context.Set<T>().CountAsync();
           var registros = await _context.Set<T>()
           .Skip((pageIndex -1)*pageSize)
           .Take(pageSize)
           .ToListAsync();
           return (totalRegistros, registros);
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
           return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           _context.RemoveRange(entities);
        }

        public void Update(T entity)
        {
           _context.Update(entity);
        }
        public bool Exist(Expression<Func<T,bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }
        
    }
}