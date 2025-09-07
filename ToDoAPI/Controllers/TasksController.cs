using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Bll.Features.Tasks.Commands.ChangeTaskStatus;
using ToDoAPI.Bll.Features.Tasks.Commands.CreateTask;
using ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask;
using ToDoAPI.Bll.Features.Tasks.Commands.UpdateTaskPercent;
using ToDoAPI.Bll.Features.Tasks.DTOs;
using ToDoAPI.Bll.Features.Tasks.Queries.GetAllTasks;
using ToDoAPI.Bll.Features.Tasks.Queries.GetIncomingTasks;
using ToDoAPI.Bll.Features.Tasks.Queries.GetTaskById;

namespace ToDoAPI.Controllers
{
    /// <summary>
    /// Task controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            /// <summary>
            /// Inject mediator to be able doing any commands and queries (CQRS pattern)
            /// </summary>
            _mediator = mediator;
        }

        /// <summary>
        /// Get all tasks in database
        /// </summary>
        /// <returns>
        /// 200 - List of tasks
        /// 204 - If no any tasks
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTasksQuery());

            if (!result.Any())
                return NoContent();

            return Ok(result);
        }


        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="command">
        /// string Title - Task title - required
        /// bool IsCompleted - Determines if task is completed - required, default: false
        /// string? Description - Task description
        /// int PercentageComplete - Percentage of task completion - required, from 0 to 100, default: 0
        /// DateTime CreatedAt - Task creation date - required, default: current time
        /// DateTime? ExpiryDate - Task date until user wants to complete the task
        /// </param>
        /// <returns>201</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            if (command is null)
                return BadRequest();

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, null);
        }

        /// <summary>
        /// Retrieves a task by its unique identifier.
        /// </summary>
        /// <remarks>This method sends a <see cref="GetTaskByIdQuery"/> to the mediator to retrieve the
        /// task.  If the task is not found, a 404 Not Found response is returned.</remarks>
        /// <param name="id">The unique identifier of the task to retrieve.</param>
        /// <returns>200 - Task</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Updates existing task
        /// This method should be used both to mark task as completed, change percentage of task completion
        /// and to making any other changes in task
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <param name="command">
        /// int Id - Task ID - required
        /// string Title - Task title - required
        /// bool IsCompleted - Determines if task is completed - required, default: false
        /// string? Description - Task description
        /// int PercentageComplete - Percentage of task completion - required, from 0 to 100, default: 0
        /// DateTime CreatedAt - Task creation date - required, default: current time
        /// DateTime? ExpiryDate - Task date until user wants to complete the task
        /// </param>
        /// <returns>204</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Get tasks which user wants to complete in next 7 days
        /// </summary>
        /// <returns>
        /// 200 - if there are tasks which are expiring in next 7 days
        /// 204 - if no tasks are expiring in next 7 days
        /// </returns>
        [HttpGet("GetIncomingTasks")]
        public async Task<IActionResult> GetIncomingTasks()
        {
            var result = await _mediator.Send(new GetIncomingTasksQuery());

            if (!result.Any())
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        /// Update only task percentage completion
        /// </summary>
        /// <param name="id">Task id</param>
        /// <param name="dto">
        /// int PercentageComplete - Percentage of task completion - required, from 0 to 100
        /// </param>
        /// <returns>204</returns>
        [HttpPatch("{id}/percent")]
        public async Task<IActionResult> UpdatePercent(int id, [FromBody] UpdateTaskPercentDto dto)
        {
            var result = await _mediator.Send(new UpdateTaskPercentCommand(id, dto.PercentageComplete));

            if (!result)
                return NotFound();

            return NoContent();
        }


        /// <summary>
        /// Change only task status - mark as completed or uncompleted
        /// </summary>
        /// <param name="id">Task id</param>
        /// <param name="dto">
        /// bool IsCompleted - Determines if task is completed - required
        /// </param>
        /// <returns>204</returns>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ChangeTaskStatusDto dto)
        {
            var result = await _mediator.Send(new ChangeTaskStatusCommand(id, dto.IsCompleted));

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}