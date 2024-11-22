using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WorkToDo.Models;
using WorkToDo.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace WorkToDo.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public IActionResult Index()
        {
            // Retrieve all tasks assigned to the logged-in user
            var userId = User.Identity?.Name; // Assuming "Name" is the username or email
            var tasks = _context.Assignment
                .Where(t => t.AssignedTo == userId)
                .OrderBy(t => t.DueDate)
                .ToList();

            return View(tasks);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Assignment.Add(assignment);
                _context.SaveChanges();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Details), new { id = assignment.TaskId });
            }
            return View(assignment);
        }

        // Additional actions for Create, Edit, Delete, etc., can be added here
    }
}
