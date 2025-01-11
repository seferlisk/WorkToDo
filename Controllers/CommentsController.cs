using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkToDo.Models;
using WorkToDo.Services;

namespace WorkToDo.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IUserContext _userContext;

        public CommentsController(ICommentService commentService, IUserService userService, IUserContext userContext)
        {
            _commentService = commentService;
            _userService = userService;
            _userContext = userContext;
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

            var userId = _userContext.GetCurrentUserId(); // Get the logged-in user ID

            try
            {
                var appUserId = _userService.GetOrCreateApplicationUser(userId).ApplicationUserId;
                _commentService.AddComment(workItemId, content, appUserId);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the comment.");
            }

            return RedirectToAction("Details", "WorkItem", new { id = workItemId });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
