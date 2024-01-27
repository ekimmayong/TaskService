using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Models;

namespace TaskService.Domain.Interfaces.Context
{
    public interface IApplicationContext
    {
        DbSet<TaskModel> Tasks { get; set; }

        Task<int> SaveChangesAsync();
        EntityEntry<T> Entry<T>(T entity) where T : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SetModified(object entity);
    }
}
