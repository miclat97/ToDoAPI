using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Bll.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand : IRequest<int>
    {
        public required string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? Description { get; set; }
        public int PercentageComplete { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
    }
}