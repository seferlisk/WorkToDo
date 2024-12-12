using Microsoft.EntityFrameworkCore;
using WorkToDo.Data;
using WorkToDo.DTO;
using WorkToDo.Models;

namespace WorkToDo.Services
{
    public class TaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WorkItem> GetAllTasksForUser(string userId)
        {
            return _context.WorkItem
                //.Where(t => t.AssignedTo == userId)
                .OrderBy(t => t.DueDate)
                .ToList();
        }

        public List<Category> GetAllCategories()
        {
            return _context.Category.ToList();
        }

        public WorkItem GetTaskDetails(int id)
        {
            return _context.WorkItem
                .Include(w => w.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefault(w => w.WorkItemId == id);
        }

        public async Task<WorkItem> GetTaskByIdAsync(int id)
        {
            return await _context.WorkItem.FindAsync(id);
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
            var task = await _context.WorkItem.FindAsync(id);
            if (task == null)
                return false;

            _context.WorkItem.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public void CreateTask(WorkItem task)
        {
            _context.WorkItem.Add(task);
            _context.SaveChanges();
        }

        public bool TaskExists(int id)
        {
            return _context.WorkItem.Any(e => e.WorkItemId == id);
        }
    }
}
