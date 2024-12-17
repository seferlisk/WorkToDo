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
            
            try
            {
                // Use the CommentService to add a new comment
                _commentService.AddComment(workItemId, content, userId);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors gracefully
                ModelState.AddModelError(string.Empty, "An error occurred while saving the comment.");
                return RedirectToAction("Details", "WorkItem", new { id = workItemId });
            }

            return RedirectToAction("Details", "WorkItem", new { id = workItemId });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
