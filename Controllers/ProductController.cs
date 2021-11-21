using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rocy;
using Rocy.Data;
using Rocy.Models;
using Rocy.Models.ViewModels;

namespace Rocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment webHostEnvirement;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvirement)
        {
            this.webHostEnvirement = webHostEnvirement;
            this._db = db;

        }
        public IActionResult Index()
        {
            var product = _db.product.Include(c=>c.Category).Include(a=>a.ApplicationType).ToList();
            // foreach (var item in product)
            // {
            //     item.Category = _db.Category.FirstOrDefault(c => c.Id == item.CategoryId);
            //     item.ApplicationType=_db.ApplicationTypes.FirstOrDefault(a=>a.Id==item.ApplicationTypeId);
            // }
            return View(product);
        }
        [HttpGet]
        public IActionResult Create(int? id)
        {

            ProductVM productVM = new ProductVM();
            productVM.CategoryList = _db.Category.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            productVM.ApplicationType=_db.ApplicationTypes.Select(a=>new SelectListItem(){
                Text=a.Name,
                Value=a.Id.ToString()
            });
        

            if (id != 0)
            {
                //id is Exist
                productVM.product = _db.product.Find(id);
            }
            else
            {
                // new product
                productVM.product = new Product();
            }






            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            if (!ModelState.IsValid)
                return View(productVM);

            var fullImagePath = webHostEnvirement.WebRootPath + AppConst.ProductImagePath;
            var files = HttpContext.Request.Form.Files;
            var fileName = Guid.NewGuid().ToString();
            var exetention = Path.GetExtension(files[0].FileName);


            if (productVM.product.Id == 0)
            {
                //Create
                productVM.product.Image = fileName + exetention;
                _db.product.Add(productVM.product);
            }
            else
            {//update
                var productDb = _db.product.Find(productVM.product.Id);
                if (files.Count > 0)
                {
                    var oldFile = Path.Combine(fullImagePath, productDb.Image);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                    productDb.Image = fileName + exetention;
                }
                productDb.Name=productVM.product.Name;
                productDb.ApplicationTypeId=productVM.product.ApplicationTypeId;
                productDb.CategoryId=productVM.product.CategoryId;
                productDb.Description=productVM.product.Description;

            }

            using (var fileStream = new FileStream(Path.Combine(fullImagePath, fileName + exetention), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }




            _db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Product = _db.product.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (Product == null)
                return NotFound();

            return View(Product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var Product = _db.product.Find(id);
            
            var fullImagePath = webHostEnvirement.WebRootPath + AppConst.ProductImagePath;
            var oldFile = Path.Combine(fullImagePath, Product.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.product.Remove(Product);
            _db.SaveChanges();
            return RedirectToAction("index");
        }





    }
}