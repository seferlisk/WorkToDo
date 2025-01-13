using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorkToDo.Data;
using WorkToDo.DTO;
using WorkToDo.Models;
using WorkToDo.Services;

namespace WorkToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IUserContext _userContext;
        private readonly ITaskService _taskService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, IUserService userService, IUserContext userContext,ITaskService taskService, ILogger<HomeController> logger)
        {
            _context = context;
            _userService = userService;
            _userContext = userContext;
            _taskService = taskService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if the user is logged in
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogWarning("Unauthenticated user attempted to access Index.");
                return Challenge(); // Redirects to the login page
            }

            try
            {
                // Get the current user's identity ID
                var identityUserId = _userContext.GetCurrentUserId();

                if (string.IsNullOrEmpty(identityUserId))
                {
                    _logger.LogError("Authenticated user has no valid identity ID.");
                    return View("Error"); // Handle the case where user ID is null
                }

                // Ensure the user is created or associated
                var user = _userService.GetOrCreateApplicationUser(identityUserId);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while associating the user.");
                return View("Error"); // Redirect to an error page
            }

            return View();
        }

        public IActionResult Features()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            // Check if the user is logged in
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogWarning("Unauthenticated user attempted to access the Dashboard.");
                return View("Error"); // Redirect to an error page or a login page
            }

            try
            {
                // Get the current user ID using IUserContext
                var identityUserId = _userContext.GetCurrentUserId();

                if (string.IsNullOrEmpty(identityUserId))
                {
                    _logger.LogError("Authenticated user has no valid identity ID.");
                    return View("Error");
                }

                // Get the application user from IUserService
                var user = _userService.GetOrCreateApplicationUser(identityUserId);

                // Fetch tasks for the user 
                var tasks = _taskService.GetAllTasksForUser(user.ApplicationUserId);

                // Calculate summary
                var pendingTasks = tasks.Count(t => !t.IsCompleted && t.DueDate >= DateTime.Now);
                var completedTasks = tasks.Count(t => t.IsCompleted);
                var overdueTasks = tasks.Count(t => !t.IsCompleted && t.DueDate < DateTime.Now);

                // Get upcoming tasks
                var upcomingTasks = tasks
                    .Where(t => t.DueDate >= DateTime.Now && t.DueDate <= DateTime.Now.AddDays(7))
                    .OrderBy(t => t.DueDate)
                    .ToList();

                // Pass data to the view
                var model = new DashboardDTO
                {
                    //UserName = user.Name, // Replace with the appropriate property
                    PendingTasks = pendingTasks,
                    CompletedTasks = completedTasks,
                    OverdueTasks = overdueTasks,
                    UpcomingTasks = upcomingTasks
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading the Dashboard.");
                return View("Error");
            }
        }
    }
}
