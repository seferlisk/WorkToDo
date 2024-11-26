using System.ComponentModel.DataAnnotations;

namespace WorkToDo.Models
{
    public class Assignment
    {
        public int TaskId { get; set; }
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public PriorityLevel Priority { get; set; }        
        public int CategoryId { get; set; } // Foreign key for Category
        public Category Category { get; set; }
        public string AssignedTo { get; set; } // Link to User
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Urgent
    }

}
