using WorkToDo.Models;

namespace WorkToDo.Services
{
    public interface IUserService
    {
        ApplicationUser GetOrCreateApplicationUser(string identityUserId);
    }
}
