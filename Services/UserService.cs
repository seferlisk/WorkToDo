using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WorkToDo.Data;
using WorkToDo.Models;

namespace WorkToDo.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser? GetOrCreateApplicationUser (string identityUserId)
        {
            var applicationUser = _context.ApplicationUsers.FirstOrDefault(f => f.IdentityUserId == identityUserId);
            
            if (applicationUser == null)
            {
                _context.ApplicationUsers.Add(new ApplicationUser()
                {
                     IdentityUserId = identityUserId,
                });
            }
            return _context.ApplicationUsers.FirstOrDefault(f => f.IdentityUserId == identityUserId);
        }
        
    }
}
