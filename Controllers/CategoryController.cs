using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rocy.Data;

namespace Rocy.Controllers
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
          var cat=  _db.Category.ToList();
            return View(cat);
        }
    }
}