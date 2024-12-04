using Microsoft.AspNetCore.Identity;
namespace WorkToDo.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public ICollection<Assignment> AssignedTasks { get; set; } // Tasks assigned to the user

    }

}
