using System.ComponentModel.DataAnnotations;

namespace WorkToDo.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }


        public int? WorkItemId { get; set; }
        public WorkItem? WorkItem { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

}
