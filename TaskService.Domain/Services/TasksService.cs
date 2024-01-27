using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Interfaces.BaseRepository;
using TaskService.Domain.Interfaces.IServices;
using TaskService.Domain.Models;

namespace TaskService.Domain.Services
{
    public class TasksService : ITasksService
    {
        private readonly IBaseRepository<TaskModel> _taskRepository;
        public TasksService(IBaseRepository<TaskModel> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task CreateNewTask(TaskModel model)
        {
            await _taskRepository.AddAsync(model);
            await _taskRepository.SaveChangesAsync();
        }

        public async Task DeleteTask(int id)
        {
            var item = await _taskRepository.GetByIdAsync(id);
            if(item != null)
            {
                await _taskRepository.DeleteAsync(item);
                await _taskRepository.SaveAsync();
            }
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskModel?> GetTaskById(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<TaskModel> UpdateTask(int id, TaskModel model)
        {
            var item = await _taskRepository.GetByIdAsync(id);
            if(item != null)
            {
                item.TaskName = model.TaskName;
                item.Description = model.Description;
                item.IsComplete = model.IsComplete;
                item.IsActive = model.IsActive;

                await _taskRepository.UpdateAsync(item);
                await _taskRepository.SaveAsync();
            }

            return model;
        }

        public async Task<string> MarkTaskCompleted(int id)
        {
            var item = await _taskRepository.GetByIdAsync(id);

            if(item != null && item.IsComplete != true)
            {
                item.IsComplete = true;
                item.CompletedTimeStamp = DateTime.UtcNow;

                _taskRepository.SetModified(item);
                await _taskRepository.SaveAsync();

                return "Task Completed";
            }

            return "No Task to complete or task already completed";
        }
    }
}
