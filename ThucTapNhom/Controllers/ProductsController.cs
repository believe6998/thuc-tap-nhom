using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThucTapNhom.Models;

namespace ThucTapNhom.Controllers
{
    public class ProductsController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Products
        public ActionResult Index(int? categoryId)
        {
            var products = db.Products.AsQueryable();
            if(categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }
            return View(products.ToList());
        }

        [ChildActionOnly]
        public ActionResult MenuCategory()
        {
            //var model = new DanhMucF().DSDanhMuc.ToList();
            return PartialView(db.Categories.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

    }
        
}
