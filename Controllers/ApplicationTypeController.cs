using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rocy.Data;
using Rocy.Models;

namespace Rocky.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationTypeController(ApplicationDbContext db)
        {
            this._db = db;

        }
        public IActionResult Index()
        {
            var App = _db.ApplicationTypes.ToList();
            return View(App);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType App)
        {
            if (!ModelState.IsValid)
                return View();

            _db.ApplicationTypes.Add(App);
            _db.SaveChanges();
            return RedirectToAction("index");
        }


    }
}