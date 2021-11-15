using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rocy.Data;
using Rocy.Models;

namespace Rocky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;

        }
        public IActionResult Index()
        {
            var cat = _db.Category.ToList();
            return View(cat);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category Cat)
        {
            if (!ModelState.IsValid)
                return View();

            _db.Category.Add(Cat);
            _db.SaveChanges();
            return RedirectToAction("index");
        }


    }
}