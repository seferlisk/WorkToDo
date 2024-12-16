using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkToDo.Models;
using WorkToDo.Services;

namespace WorkToDo.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int workItemId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError("Content", "Comment content cannot be empty.");
                return RedirectToAction("Details", "WorkItem", new { id = workItemId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user ID
            var comment = new Comment
            {
                Content = content,
                CreatedAt = DateTime.Now,
                WorkItemId = workItemId,
                UserId = userId
            };

            _context.Comment.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", "WorkItem", new { id = workItemId });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
