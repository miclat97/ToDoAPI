using MediatR;
using ToDoAPI.Bll.Features.Tasks.DTOs;

namespace ToDoAPI.Bll.Features.Tasks.Queries.GetAllTasks
{
    public record GetAllTasksQuery : IRequest<IEnumerable<TaskDto>>;
}