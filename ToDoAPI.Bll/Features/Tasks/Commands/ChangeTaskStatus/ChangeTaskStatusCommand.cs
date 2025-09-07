using MediatR;

namespace ToDoAPI.Bll.Features.Tasks.Commands.ChangeTaskStatus
{
    public record ChangeTaskStatusCommand(int Id, bool IsCompleted) : IRequest<bool>;
}
