﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

        public async Task<TaskModel> CreateNewTask(TaskModel model)
        {

            var result = await _taskRepository.AddAsync(model);
            await _taskRepository.SaveChangesAsync();

            return result;
        }

        public async Task<string> DeleteTask(int id)
        {
            var item = await _taskRepository.GetByIdAsync(id);
            if(item != null)
            {
                await _taskRepository.DeleteAsync(item);
                await _taskRepository.SaveChangesAsync();

                return "Deleted Sucessfully.";
            }

            return "Item not found.";
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskModel?> GetTaskById(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<string> UpdateTask(int id, TaskModel model)
        {
            var item = await _taskRepository.GetByIdAsync(id);
            if(item != null && item.IsActive)
            {
                item.TaskName = model.TaskName;
                item.Description = model.Description;
                item.IsComplete = model.IsComplete;
                item.IsActive = model.IsActive;
                item.RowVersion = Encoding.ASCII.GetBytes(DateTime.UtcNow.ToString());

                await _taskRepository.UpdateAsync(item, model.RowVersion);
                await _taskRepository.SaveChangesAsync();

                return "Successfully Updated Data";
            }

            return "Task is not Active";
        }

        public async Task<string> MarkTaskCompleted(int id, string rowVersion)
        {
            var item = await _taskRepository.GetByIdAsync(id);
            byte[] rowVersionConvert = Encoding.ASCII.GetBytes(DateTime.Parse(rowVersion).ToString());

            if(item != null && item.IsComplete != true)
            {
                item.IsComplete = true;
                item.IsActive = false;
                item.CompletedTimeStamp = DateTime.UtcNow;
                item.RowVersion = Encoding.ASCII.GetBytes(DateTime.UtcNow.ToString());

                _taskRepository.SetModified(item, rowVersionConvert);
                await _taskRepository.SaveChangesAsync();

                return "Task Completed";
            }

            return "Task already completed";
        }
    }
}
