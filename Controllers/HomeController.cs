using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rocy.Data;
using Rocy.Models;
using Rocy.Models.ViewModels;
using Rocy.Utility;

namespace Rocy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            this._db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {

            HomeVm homeVm = new HomeVm()
            {
                Products = _db.product.Include(c => c.Category).Include(a => a.ApplicationType),
                Categotirs = _db.Category
            };


            return View(homeVm);
        }
        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCart = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart) != null && HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart).Count() > 0)
            {
                shoppingCart = HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart);
            }

            DetailsVm details = new DetailsVm()
            {
                Product = _db.product.Include(c => c.Category).Include(a => a.ApplicationType).FirstOrDefault(p => p.Id == id),
                ExistsInCart = false

            };
            foreach (var item in shoppingCart)
            {
                if (item.ProductId == id)
                {
                    details.ExistsInCart = true;
                }
            }
            return View(details);
        }
        [HttpPost(), ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {

            List<ShoppingCart> shoppingCart = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart) != null && HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart).Count() > 0)
            {
                shoppingCart = HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart);
            }
            shoppingCart.Add(new ShoppingCart() { ProductId = id });
            HttpContext.Session.Set(AppConst.SessionCart, shoppingCart);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult RemoveFromCart(int id)
        {

            List<ShoppingCart> shoppingCart = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart) != null && HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart).Count() > 0)
            {
                shoppingCart = HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart);
            }
            var removeitem = shoppingCart.SingleOrDefault(p => p.ProductId == id);
            if (removeitem != null)
            {
                shoppingCart.Remove(removeitem);
            }

            HttpContext.Session.Set(AppConst.SessionCart, shoppingCart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
