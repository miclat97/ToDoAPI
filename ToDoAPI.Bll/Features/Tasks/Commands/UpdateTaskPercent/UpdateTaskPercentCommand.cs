using MediatR;

namespace ToDoAPI.Bll.Features.Tasks.Commands.UpdateTaskPercent
{
    public record UpdateTaskPercentCommand(int Id, int PercentageComplete) : IRequest<bool>;
}
