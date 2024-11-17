namespace WorkToDo.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TaskId { get; set; } // Foreign key for Task
        public Assignment Task { get; set; }
        public int UserId { get; set; } // Foreign key for User
        public User User { get; set; }
    }

}
