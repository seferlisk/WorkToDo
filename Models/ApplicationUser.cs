using Microsoft.AspNetCore.Identity;
namespace WorkToDo.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public ICollection<WorkItem>? Tasks { get; set; } // Tasks assigned to the user
        public ICollection<Comment>? Comments { get; set; } // Tasks assigned to the user

    }

}
