namespace WorkToDo.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public PriorityLevel Priority { get; set; }
        public string AssignedTo { get; set; } // Link to User if needed
        public int CategoryId { get; set; } // Foreign key for Category
        public Category Category { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Urgent
    }

}
