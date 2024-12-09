using Microsoft.AspNetCore.Mvc;

namespace WorkToDo.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
