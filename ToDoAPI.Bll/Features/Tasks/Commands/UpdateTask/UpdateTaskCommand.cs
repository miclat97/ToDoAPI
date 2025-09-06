using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand : IRequest<bool>
    {
        public required int Id { get; set; }
        public string? Title { get; set; }
        public bool? IsCompleted { get; set; }
        public string? Description { get; set; }
        public int? PercentageComplete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}