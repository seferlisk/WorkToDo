using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkToDo.Models;
using WorkToDo.Services;

namespace WorkToDo.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CommentService _commentService;
        private readonly UserService _userService;

        public CommentsController(CommentService commentService, UserService userService)
        {
            _commentService = commentService;
            _userService = userService;
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

            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user ID
            
            try
            {
                // Use the CommentService to add a new comment
                _commentService.AddComment(workItemId, content, _userService.GetOrCreateApplicationUser(identityUserId).ApplicationUserId);
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
