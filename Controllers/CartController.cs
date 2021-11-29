using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocy.Data;
using Rocy.Models;
using Rocy.Utility;

namespace Rocy.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            this._db = db;

        }

        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCart=new List<ShoppingCart> ();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(AppConst.SessionCart)!=null
            && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(AppConst.SessionCart).Count()>0
            )
            {
                shoppingCart=HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart);    
            }
                List<int> prodIds=shoppingCart.Select(p=>p.ProductId).ToList();
                var products=_db.product.Where(p=>prodIds.Contains(p.Id));


            return View(products);
        }

        public IActionResult Remove(int Id){

             List<ShoppingCart> shoppingCart=new List<ShoppingCart> ();
             shoppingCart=HttpContext.Session.Get<List<ShoppingCart>>(AppConst.SessionCart);

             var RemoveProduct=shoppingCart.Where(s=>s.ProductId==Id).FirstOrDefault();
             shoppingCart.Remove(RemoveProduct);
             HttpContext.Session.Set(AppConst.SessionCart,shoppingCart);
            return RedirectToAction(nameof(Index));


        }
    }
}