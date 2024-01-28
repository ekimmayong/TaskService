using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Models;

namespace TaskService.Domain.Interfaces.IServices
{
    public interface ITasksService
    {
        Task<TaskModel> CreateNewTask(TaskModel model);
        Task<IEnumerable<TaskModel>> GetAllTasks();

        Task<TaskModel?> GetTaskById(int id);

        Task DeleteTask(int id);

        Task<TaskModel> UpdateTask(int id, TaskModel model);

        Task<string> MarkTaskCompleted(int id);
    }
}
