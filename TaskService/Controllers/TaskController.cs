using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskService.Domain.Interfaces.IServices;
using TaskService.Domain.Models;
using TaskService.Infrastructure.Models.Task;

namespace TaskService.Controllers
{
    [Route("api/task")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITasksService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITasksService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all-task")]
        public async Task<IActionResult> GetAllTask()
        {
            var tasks = await _taskService.GetAllTasks();
            var response = _mapper.Map<IEnumerable<TaskModelDTO>>(tasks);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-task-by-id")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            var response = _mapper.Map<TaskModelDTO>(task);

            return Ok(response);
        }

        [HttpPost]
        [Route("create-task")]
        public async Task<IActionResult> CeateNewTask(TaskModelDTO model)
        {
            if(ModelState.IsValid)
            {
                var response = _mapper.Map<TaskModel>(model);

                await _taskService.CreateNewTask(response);

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("update-task")]
        public async Task<IActionResult> UpdateTask(TaskModelDTO model)
        {
            if(ModelState.IsValid)
            {
                var task = _mapper.Map<TaskModel>(model);
                var response = await _taskService.UpdateTask(task);

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPatch]
        [Route("mark-task-completed")]
        public async Task<IActionResult> MarkTaskCompleted(int id)
        {
            await _taskService.MarkTaskCompleted(id);

            return Ok();
        }
    }
}
