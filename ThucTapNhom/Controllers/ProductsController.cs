using PagedList;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ThucTapNhom.Models;

namespace ThucTapNhom.Controllers
{
    public class ProductsController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Products
        public ActionResult Index(int? page, int? categoryId/*string sortOrder, string currentFilter, string searchString,*/)
        {
            var products = db.Products.AsQueryable();
            ViewBag.categoryId = categoryId;
            //ViewBag.CurrentSort = sortOrder;

            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //ViewBag.CurrentFilter = searchString;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    products = products.Where(p => p.Name.Contains(searchString));
            //}

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
            //return View(products.ToList());
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
