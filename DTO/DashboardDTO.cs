using WorkToDo.Models;

namespace WorkToDo.DTO
{
    public class DashboardDTO
    {
        public string UserName { get; set; }
        public int PendingTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int OverdueTasks { get; set; }
        public List<WorkItem> UpcomingTasks { get; set; }
    }
}
