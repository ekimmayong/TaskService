using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Interfaces.Context;
using TaskService.Domain.Models;

namespace TaskService.Infrastructure.Context
{
    public class ApplicationDbContext: DbContext, IApplicationContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<TaskModel> Tasks { get; set; }

        public void SetModified(object entity)
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
