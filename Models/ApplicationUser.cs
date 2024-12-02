using Microsoft.AspNetCore.Identity;
namespace WorkToDo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Assignment> AssignedTasks { get; set; } // Tasks assigned to the user
    }

}
