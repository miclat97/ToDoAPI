using MediatR;
using ToDoAPI.Bll.Features.Tasks.DTOs;

namespace ToDoAPI.Bll.Features.Tasks.Queries.GetIncomingTasks
{
    public record GetIncomingTasksQuery : IRequest<IEnumerable<TaskDto>>;
}
