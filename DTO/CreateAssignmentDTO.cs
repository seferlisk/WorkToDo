using System.ComponentModel.DataAnnotations;

namespace WorkToDo.DTO
{
    public class CreateAssignmentDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Priority { get; set; }

        public string AssignedTo { get; set; } = string.Empty;
        
        [Required]
        public int CategoryId { get; set; }
    }

}
