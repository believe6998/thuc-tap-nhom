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
    public class CoachesController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Coaches
        public ActionResult Index()
        {
            return View(db.Coaches.ToList());
        }
    }
}
