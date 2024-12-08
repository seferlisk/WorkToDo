using System.ComponentModel.DataAnnotations;

namespace WorkToDo.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<WorkItem>? WorkItems { get; set; } // Navigation property
    }

}
