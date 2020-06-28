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
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: MenuCategory
        public ActionResult Category(int id)
        {
            return View("Index", db.Categories.Where(c => c.Id == id).ToList());
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
