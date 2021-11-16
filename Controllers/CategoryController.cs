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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category=_db.Category.Find(id);
            if(category ==null)
            return NotFound();

            return View(category);
        }
         [HttpPost]
          [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat)
        {
            if(!ModelState.IsValid)
            return View(cat);

            var category=_db.Category.Find(cat.Id);
            category.Name=cat.Name;
            category.Displayorder=cat.Displayorder;
            _db.SaveChanges();
            
            return RedirectToAction("index");
        }


         


    }
}