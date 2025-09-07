using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Bll.Features.Tasks.DTOs
{
    public class UpdateTaskPercentDto
    {
        [Range(0, 100)]
        public int PercentageComplete { get; set; }
    }

}
