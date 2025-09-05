namespace ToDoAPI.Bll.Features.Tasks.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public int PercentageComplete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}