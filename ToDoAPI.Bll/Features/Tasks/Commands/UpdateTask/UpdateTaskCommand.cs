using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        [Range(0, 100)]
        public int PercentageComplete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}