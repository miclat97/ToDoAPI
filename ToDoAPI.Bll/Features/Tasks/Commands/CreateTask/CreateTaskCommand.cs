using MediatR;

namespace ToDoAPI.Bll.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand : IRequest<int>
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public DateTime? ExpiryDate { get; init; }
    }
}