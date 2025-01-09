using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkToDo.Models;
using WorkToDo.Data;
using System.Threading.Tasks;
using WorkToDo.DTO;
using WorkToDo.Services;

namespace WorkToDo.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: Assignment
        public IActionResult Index()
        {
            // Retrieve all tasks assigned to the logged-in user
            var userId = User.Identity?.Name; // Assuming "Name" is the username or email

            var tasks = _taskService.GetAllTasksForUser(userId);

            return View(tasks);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            var dto = new CreateTaskDTO
            {
                Categories = _taskService.GetAllCategories()
            };

            return View(dto);
        }

        // POST: Task/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(CreateTaskDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        dto.Categories = _taskService.GetAllCategories(); // Reload categories if validation fails

        //        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //        {
        //            Console.WriteLine(error.ErrorMessage); // Log errors for debugging
        //        }

        //        return View(dto); // Return the view with validation errors
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Convert string to PriorityLevel enum
        //        if (!Enum.TryParse(dto.Priority, out PriorityLevel priority))
        //        {
        //            ModelState.AddModelError("Priority", "Invalid priority level.");
        //            dto.Categories = _taskService.GetAllCategories();
        //            return View(dto);
        //        }

        //        var task = new WorkItem
        //        {
        //            Title = dto.Title,
        //            Description = dto.Description,
        //            DueDate = dto.DueDate,
        //            Priority = priority, // Map to enum
        //            IsCompleted = false, // Default value
        //            UserId = dto.UserId,
        //            CategoryId = dto.CategoryId
        //        };

        //        _taskService.CreateTask(task);

        //        return RedirectToAction(nameof(Details), new { id = task.WorkItemId });
        //    }
        //    return View(dto);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTaskDTO dto)
        {
            // Reload categories for the dropdown in case validation fails
            dto.Categories = _taskService.GetAllCategories();

            // Validate the model
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Log validation errors for debugging
                }
                return View(dto); // Return the view with validation errors
            }

            // Map DTO to WorkItem
            var task = new WorkItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority, // Nullable PriorityLevel
                IsCompleted = false, // Default value for new tasks
                CategoryId = dto.CategoryId, // Links the task to a category
                UserId = dto.UserId // Optional field, can be null
            };

            // Save the new task using the service
            _taskService.CreateTask(task);

            // Redirect to the details page of the newly created task
            return RedirectToAction(nameof(Details), new { id = task.WorkItemId });
        }

        public IActionResult Details(int id)
        {
            var task = _taskService.GetTaskDetails(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var task = await _taskService.GetTaskByIdAsync(id.Value);
            if (task == null)
                return NotFound();

            // Map to a DTO
            var editDto = new EditTaskDTO
            {
                WorkItemId = task.WorkItemId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority.ToString()
                //AssignedTo = assignment.AssignedTo,
                //CategoryId = assignment.CategoryId
            };

            return View(editDto);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTaskDTO dto)
        {
            if (id != dto.WorkItemId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            if (!Enum.TryParse(dto.Priority, out PriorityLevel priority))
            {
                ModelState.AddModelError("Priority", "Invalid priority level.");
                return View(dto);
            }

            var task = new WorkItem
            {
                WorkItemId = id,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = priority
            };

            var success = await _taskService.UpdateTaskAsync(task);
            if (!success)
            {
                if (!_taskService.TaskExists(id))
                    return NotFound();

                throw new DbUpdateConcurrencyException();
            }

            return RedirectToAction(nameof(Details), new { id = dto.WorkItemId });
            //if (id != dto.WorkItemId)
            //{
            //    return BadRequest();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var assignment = await _context.WorkItem.FindAsync(id);
            //        if (assignment == null)
            //        {
            //            return NotFound();
            //        }

            //        // Convert Priority string to PriorityLevel enum
            //        if (!Enum.TryParse(dto.Priority, out PriorityLevel priority))
            //        {
            //            ModelState.AddModelError("Priority", "Invalid priority level.");
            //            // Repopulate categories before returning view
            //            ViewBag.Categories = await _context.Category.ToListAsync();
            //            return View(dto);
            //        }

            //        // Update properties
            //        assignment.Title = dto.Title;
            //        assignment.Description = dto.Description;
            //        assignment.DueDate = dto.DueDate;
            //        assignment.Priority = priority;
            //        //assignment.AssignedTo = dto.AssignedTo;
            //        //assignment.CategoryId = dto.CategoryId;

            //        _context.Update(assignment);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!AssignmentExists(dto.WorkItemId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Details), new { id = dto.WorkItemId });
            //}

            //// If model state is invalid, repopulate categories
            ////ViewBag.Categories = await _context.Category.ToListAsync();

            //return View(dto);
        }        

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var task = await _taskService.GetTaskByIdAsync(id.Value);
            if (task == null)
                return NotFound();

            return View(task);
        }


        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
