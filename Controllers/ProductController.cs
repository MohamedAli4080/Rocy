using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rocy.Data;
using Rocy.Models;

namespace Rocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            this._db = db;

        }
        public IActionResult Index()
        {
            var product = _db.product.ToList();
            foreach (var item in product)
            {
                item.Category=_db.Category.FirstOrDefault(c=>c.Id==item.CategoryId);
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            _db.product.Add(product);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Product=_db.product.Find(id);
            if(Product ==null)
            return NotFound();

            return View(Product);
        }
         [HttpPost]
          [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if(!ModelState.IsValid)
            return View(product);

            var productdb=_db.Category.Find(product.Id);
            productdb.Name=product.Name;
           
            _db.SaveChanges();
            
            return RedirectToAction("index");
        }


         


    }
}