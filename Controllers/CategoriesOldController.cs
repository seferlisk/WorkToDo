using Microsoft.AspNetCore.Mvc;
using WorkToDo.Data;
using WorkToDo.DTO;
using WorkToDo.Models;

namespace WorkToDo.Controllers
{
    public class CategoriesOldController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesOldController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve all categories from the database
            var categories = _context.Categories.ToList();

            // Pass the categories to the view
            return View(categories);
        }

        // GET: Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Display the empty form for creating a new category
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // Add the new category to the database
                _context.Categories.Add(category);
                _context.SaveChanges();

                // Redirect to the Index action after successful creation
                return RedirectToAction(nameof(Index));
            }

            // If the model is invalid, redisplay the form with validation messages
            return View(category);
        }

        // GET: Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int CategoryId)
        {
            var category = _context.Categories.Find(CategoryId);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
