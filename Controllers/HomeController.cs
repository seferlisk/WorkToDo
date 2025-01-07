using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorkToDo.Data;
using WorkToDo.DTO;
using WorkToDo.Models;

namespace WorkToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        //public IActionResult Dashboard()
        //{
        //    // Get the current user ID
        //    var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        // If the user is not logged in or the user ID is null
        //        return View("Error");
        //    }

        //    // Fetch user tasks
        //    var tasks = _context.WorkItem.Where(t => t.UserId == userId).ToList();

        //    // Calculate summary
        //    var pendingTasks = tasks.Count(t => !t.IsCompleted && t.DueDate >= DateTime.Now);
        //    var completedTasks = tasks.Count(t => t.IsCompleted);
        //    var overdueTasks = tasks.Count(t => !t.IsCompleted && t.DueDate < DateTime.Now);

        //    // Get upcoming tasks
        //    var upcomingTasks = tasks
        //        .Where(t => t.DueDate >= DateTime.Now && t.DueDate <= DateTime.Now.AddDays(7))
        //        .OrderBy(t => t.DueDate)
        //        .ToList();

        //    // Pass data to the view
        //    var model = new DashboardDTO
        //    {
        //        UserName = User.Identity.Name, // Username
        //        PendingTasks = pendingTasks,
        //        CompletedTasks = completedTasks,
        //        OverdueTasks = overdueTasks,
        //        UpcomingTasks = upcomingTasks
        //    };

        //    return View(model);
        //}
    }
}
