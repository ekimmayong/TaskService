using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Text;
using TaskService.Domain.Interfaces.IServices;
using TaskService.Domain.Models;
using TaskService.Infrastructure.Models.Task;

namespace TaskService.Controllers
{
    [Route("api/task")]
    [ApiController]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskModelDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllTask()
        {
            var tasks = await _taskService.GetAllTasks();
            var response = _mapper.Map<IEnumerable<TaskModelDTO>>(tasks);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-task/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaskModelDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            var response = _mapper.Map<TaskModelDTO>(task);

            return Ok(response);
        }

        [HttpPost]
        [Route("create-task")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaskModelDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateNewTask(TaskModelDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = _mapper.Map<TaskModel>(model);

                var result = await _taskService.CreateNewTask(response);

                var mapResponse = _mapper.Map<TaskModelDTO>(result);
                return Ok(mapResponse);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("update-task/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateTask(int id, TaskModelDTO model)
        {
            if(ModelState.IsValid)
            {
                var task = _mapper.Map<TaskModel>(model);
                var response = await _taskService.UpdateTask(id, task);

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPatch]
        [Route("mark-task-completed/{id}/{rowVersion}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> MarkTaskCompleted(int id, string rowVersion)
        {
            var response = await _taskService.MarkTaskCompleted(id, rowVersion);

            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-task/{id}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var response = await _taskService.DeleteTask(id);

            return Ok(response);
        }
    }
}
