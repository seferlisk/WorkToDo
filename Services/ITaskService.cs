using WorkToDo.Models;

namespace WorkToDo.Services
{
    public interface ITaskService
    {
        List<WorkItem> GetAllTasksForUser(int applicationUserId);
        List<Category> GetAllCategories();
        Task<WorkItem> GetTaskByIdAsync(int id);
        Task<bool> UpdateTaskAsync(WorkItem task);
        Task<bool> DeleteTaskAsync(int id);
        void CreateTask(WorkItem task);
        WorkItem GetTaskDetails(int id);
        bool TaskExists(int id);
    }
}
