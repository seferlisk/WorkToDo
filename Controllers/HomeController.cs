using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorkToDo.Data;
using WorkToDo.Models;

namespace WorkToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context; 

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dashboard()
        {
            // Get current user
            var userId = User.Identity.Name; // Assuming username is unique

            // Fetch user tasks
            var tasks = _context.Tasks.Where(t => t.UserId == userId).ToList();

            // Calculate summary
            var pendingTasks = tasks.Count(t => !t.IsComplete && t.DueDate >= DateTime.Now);
            var completedTasks = tasks.Count(t => t.IsComplete);
            var overdueTasks = tasks.Count(t => !t.IsComplete && t.DueDate < DateTime.Now);

            // Get upcoming tasks
            var upcomingTasks = tasks
                .Where(t => t.DueDate >= DateTime.Now && t.DueDate <= DateTime.Now.AddDays(7))
                .OrderBy(t => t.DueDate)
                .ToList();

            // Pass data to the view
            var model = new DashboardDTO
            {
                UserName = userId,
                PendingTasks = pendingTasks,
                CompletedTasks = completedTasks,
                OverdueTasks = overdueTasks,
                UpcomingTasks = upcomingTasks
            };

            return View(model);
        }
    }
}
