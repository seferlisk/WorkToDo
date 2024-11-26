using System.ComponentModel.DataAnnotations;

namespace WorkToDo.DTO
{
    public class EditAssignmentDTO
    {
        [Required]
        public int TaskId { get; set; } // Primary Key

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Priority { get; set; }

        
        public string AssignedTo { get; set; }

        
        public int CategoryId { get; set; }
    }
}
