using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace WorkToDo.Models
{
    public class ApplicationUser
    {
        public int ApplicationUserId { get; set; }
       
        public ICollection<WorkItem>? Tasks { get; set; } // Tasks assigned to the user
        public ICollection<Comment>? Comments { get; set; } // Tasks assigned to the user

        [Required]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        
        
    }

}
