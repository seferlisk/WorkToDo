using Microsoft.EntityFrameworkCore;
using WorkToDo.Data;
using WorkToDo.DTO;
using WorkToDo.Models;

namespace WorkToDo.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WorkItem> GetAllTasksForUser(int userId)
        {
            return _context.WorkItems
                //.Where(t => t.AssignedTo == userId)
                .OrderBy(t => t.DueDate)
                .ToList();
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public WorkItem GetTaskDetails(int id)
        {
            return _context.WorkItems
                .Include(w => w.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefault(w => w.WorkItemId == id);
        }

        public async Task<WorkItem> GetTaskByIdAsync(int id)
        {
            return await _context.WorkItems.FindAsync(id);
        }

        public async Task<bool> UpdateTaskAsync(WorkItem task)
        {
            try
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.WorkItems.FindAsync(id);
            if (task == null)
                return false;

            _context.WorkItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public void CreateTask(WorkItem task)
        {
            _context.WorkItems.Add(task);
            _context.SaveChanges();
        }

        public bool TaskExists(int id)
        {
            return _context.WorkItems.Any(e => e.WorkItemId == id);
        }
    }
}
