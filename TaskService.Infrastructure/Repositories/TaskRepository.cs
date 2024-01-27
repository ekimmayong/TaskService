using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Interfaces.Context;
using TaskService.Domain.Models;

namespace TaskService.Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository<TaskModel>
    {
        public TaskRepository(IApplicationContext context) : base(context)
        {
        }
    }
}
