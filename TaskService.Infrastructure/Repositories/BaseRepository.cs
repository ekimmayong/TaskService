using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Interfaces.BaseRepository;
using TaskService.Domain.Interfaces.Context;

namespace TaskService.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IApplicationContext _context;

        public BaseRepository(IApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = _context.Set<T>();

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public Task UpdateAsync(T entity)
        {

            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void SetModified(object entity)
        {
            _context.SetModified(entity);
        }
    }
}
