using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Dal.Entities
{
    /// <summary>
    /// Task entity (model) to create db using CodeFirst approach
    /// Id - primary key
    /// Title - task title
    /// IsCompleted - determines if task is completed (in theory I think that if PercentageComplete = 100 then 
    /// IsCompleted should be true, but I left it like that to have more flexibility) - field required and by default it's false
    /// Description - task description (it can be null)
    /// PercentageComplete - percents of completion task, by default in new task it's 0; can be from 0 to 100 %
    /// CreatedAt - task creation date (by default new task has current time)
    /// ExpiryDate - date until user want to complete task (I think it can be null, if user doesn't want to specify it)
    /// </summary>
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? Description { get; set; }
        [Range(0, 100)]
        public int PercentageComplete { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
    }
}
