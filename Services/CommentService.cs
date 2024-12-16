using System.Security.Claims;
using WorkToDo.Data;
using WorkToDo.Models;

namespace WorkToDo.Services
{
    public class CommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddComment(int workItemId, string content, string userId)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Comment content cannot be empty.", nameof(content));
            }

            var comment = new Comment
            {
                Content = content,
                CreatedAt = DateTime.Now,
                WorkItemId = workItemId,
                UserId = userId
            };

            _context.Comment.Add(comment);
            _context.SaveChanges();
        }
    }
}
