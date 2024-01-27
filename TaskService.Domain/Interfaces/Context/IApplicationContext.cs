using Microsoft.EntityFrameworkCore;
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

        Task<int> SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SetModified(object entity);
    }
}
