using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThucTapNhom.Models;
using PagedList.Mvc;
using PagedList;


namespace ThucTapNhom.Controllers
{
    public class ProductsController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? categoryId, int? page)
        {
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var products = db.Products.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.ToLower().Contains(searchString));
            }


            ViewBag.categoryId = categoryId;
            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }


            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price": // Price ascending
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price); 
                    break;
                default:  // Name ascending 
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(products.ToList().ToPagedList(pageNumber, pageSize));
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
