using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Bll.Features.Tasks.Commands.CreateTask;
using ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask;
using ToDoAPI.Bll.Features.Tasks.Queries.GetAllTasks;
using ToDoAPI.Bll.Features.Tasks.Queries.GetIncomingTasks;
using ToDoAPI.Bll.Features.Tasks.Queries.GetTaskById;

namespace ToDoAPI.Controllers
{
    /// <summary>
    /// Task controller (I like this approach if comes to routing, because when project will grow to much larger I think it will be easier to manage routes)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// As I'm using CQRS pattern I need to inject mediator to be able doing any commands and queries
        /// </summary>
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTasksQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            var createdTaskId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = createdTaskId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _mediator.Send(new GetTaskByIdQuery(id));

            if (task is null)
                return NotFound();

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("GetIncomingTasks")]
        public async Task<IActionResult> GetIncomingTasks()
        {
            var result = await _mediator.Send(new GetIncomingTasksQuery());

            if (result is null)
                return NoContent();

            return Ok(result);
        }
    }
}