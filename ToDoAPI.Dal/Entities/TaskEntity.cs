using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Dal.Entities
{
    /// <summary>
    /// Entity in database representing a Task
    /// </summary>
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int PercentageComplete { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
    }
}
