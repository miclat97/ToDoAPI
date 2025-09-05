using MediatR;
using ToDoAPI.Bll.Features.Tasks.DTOs;

namespace ToDoAPI.Bll.Features.Tasks.Queries.GetTaskById
{
    public record GetTaskByIdQuery(int Id) : IRequest<TaskDto?>;
}
