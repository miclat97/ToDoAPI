using MediatR;

namespace ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand : IRequest<bool>
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public DateTime? ExpiryDate { get; init; }
    }
}