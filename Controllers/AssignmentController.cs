using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkToDo.Models;
using WorkToDo.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using WorkToDo.DTO;

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
        public IActionResult Create(CreateAssignmentDto dto)
        {
            if (ModelState.IsValid)
            {
                // Convert string to PriorityLevel enum
                if (!Enum.TryParse(dto.Priority, out PriorityLevel priority))
                {
                    ModelState.AddModelError("Priority", "Invalid priority level.");
                    return View(dto);
                }

                var assignment = new Assignment
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    DueDate = dto.DueDate,
                    Priority = priority, // Map to enum
                    IsCompleted = false // Default value
                };

                _context.Assignment.Add(assignment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = assignment.TaskId });
            }
            return View(dto);
        }

        public IActionResult Details(int id)
        {
            var assignment = _context.Assignment.FirstOrDefault(a => a.TaskId == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        public IActionResult DebugPrimaryKey()
        {
            var entityType = _context.Model.FindEntityType(typeof(Assignment));
            var primaryKey = entityType.FindPrimaryKey();

            // Output the primary key properties
            foreach (var property in primaryKey.Properties)
            {
                Console.WriteLine($"Primary Key: {property.Name}");
            }

            return Ok("Primary key information printed to console.");
        }

        public IActionResult TestPrimaryKeyBehavior()
        {
            try
            {
                var assignment1 = new Assignment
                {
                    TaskId = 1, // Explicit ID
                    Title = "Assignment 1",
                    Description = "Test Description",
                    DueDate = DateTime.Now,
                    Priority = PriorityLevel.Medium
                };

                var assignment2 = new Assignment
                {
                    TaskId = 1, // Duplicate ID to test PK constraint
                    Title = "Assignment 2",
                    Description = "Another Test Description",
                    DueDate = DateTime.Now,
                    Priority = PriorityLevel.High
                };

                // Add first assignment
                _context.Assignment.Add(assignment1);
                _context.SaveChanges();

                // Add duplicate assignment
                _context.Assignment.Add(assignment2);
                _context.SaveChanges(); // Should throw an exception
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }

            return Ok("Primary key test completed.");
        }


        // Additional actions for Create, Edit, Delete, etc., can be added here
    }
}
