using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WorkToDo.Models
{
    public class WorkItem
    {
        public int WorkItemId { get; set; }

        [Required]
        public string Title { get; set; }

        
        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }
        
        [DefaultValue(false)]
        public bool IsCompleted { get; set; }
        public PriorityLevel? Priority { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }



    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Urgent
    }

}
