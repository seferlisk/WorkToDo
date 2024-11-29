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

        // GET: Assignment
        public IActionResult Index()
        {
            // Retrieve all tasks assigned to the logged-in user
            var userId = User.Identity?.Name; // Assuming "Name" is the username or email
            var tasks = _context.Assignment
                //.Where(t => t.AssignedTo == userId)
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
            if (!ModelState.IsValid)
            {
                return View(dto); // Return the view with validation errors
            }

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
                    IsCompleted = false, // Default value
                    AssignedTo = dto.AssignedTo
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

        // GET: Assignment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var assignment = await _context.Assignment.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            // Map to a DTO
            var editDto = new EditAssignmentDTO
            {
                TaskId = assignment.TaskId,
                Title = assignment.Title,
                Description = assignment.Description,
                DueDate = assignment.DueDate,
                Priority = assignment.Priority.ToString(),
                AssignedTo = assignment.AssignedTo,
                //CategoryId = assignment.CategoryId
            };

            // Retrieve categories for the dropdown
            //ViewBag.Categories = await _context.Category.ToListAsync();

            return View(editDto);
        }

        // POST: Assignment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAssignmentDTO dto)
        {
            if (id != dto.TaskId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var assignment = await _context.Assignment.FindAsync(id);
                    if (assignment == null)
                    {
                        return NotFound();
                    }

                    // Convert Priority string to PriorityLevel enum
                    if (!Enum.TryParse(dto.Priority, out PriorityLevel priority))
                    {
                        ModelState.AddModelError("Priority", "Invalid priority level.");
                        // Repopulate categories before returning view
                        ViewBag.Categories = await _context.Category.ToListAsync();
                        return View(dto);
                    }

                    // Update properties
                    assignment.Title = dto.Title;
                    assignment.Description = dto.Description;
                    assignment.DueDate = dto.DueDate;
                    assignment.Priority = priority;
                    assignment.AssignedTo = dto.AssignedTo;
                    //assignment.CategoryId = dto.CategoryId;

                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(dto.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = dto.TaskId });
            }

            // If model state is invalid, repopulate categories
            //ViewBag.Categories = await _context.Category.ToListAsync();

            return View(dto);
        }

        // Helper method to check if Assignment exists
        private bool AssignmentExists(int id)
        {
            return _context.Assignment.Any(e => e.TaskId == id);
        }

        // GET: Assignment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.TaskId == id);

            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }


        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignment.FindAsync(id);
            if (assignment != null)
            {
                _context.Assignment.Remove(assignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
