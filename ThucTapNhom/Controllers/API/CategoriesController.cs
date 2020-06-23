using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ThucTapNhom.Models;

namespace ThucTapNhom.Controllers.Admin
{
    public class CategoriesController : ApiController
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: api/Categories
        public IHttpActionResult GetCategories(int page = 1, int size = 10, int? status = null, string name = null, string startDate = null, string endDate = null)
        {

            var skip = (page - 1) * size;

            // Get total number of records
            var total = db.Categories.Count();

            var categories = db.Categories.AsQueryable();
            if (status != null)
            {
                categories = categories.Where(s => s.Status == status);
            }
            if (name != null)
            {
                categories = categories.Where(s => s.Name.Contains(name));
            }
            if (startDate != null)
            {
                var startDateFormat = Convert.ToDateTime(startDate);
                categories = categories.Where(s => s.CreatedAt >= startDateFormat);
            }
            if (endDate != null)
            {
                var tomorrow = Convert.ToDateTime(endDate).AddDays(1);
                categories = categories.Where(s => s.CreatedAt < tomorrow);
            }
            // Select the customers based on paging parameters
            categories = categories
                .OrderBy(c => c.Id)
                .Skip(skip)
                .Take(size);

            // Return the list of customers
            return Ok(new PagedResult<Category>(categories.ToList(), page, size, total));
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }
            
            db.Entry(category).State = EntityState.Modified;
            category.UpdatedAt = DateTime.Now;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            category.Status = 1;
            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}