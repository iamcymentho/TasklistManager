using Microsoft.AspNetCore.Mvc;
using TasklistManager.Application.Dtos;
using TasklistManager.Application.Interfaces;

namespace TasklistManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewTask(TaskDto taskDto)
        {
            var createdTask = await _taskService.AddNewTask(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAllTasks()
        {
            var tasks = await _taskService.ListAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskById(string id)
        {
            var task = await _taskService.GetTaskById(id);
            return task != null ? Ok(task) : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var result = await _taskService.DeleteTask(id);
            return result ? NoContent() : NotFound();
        }
    }
}
