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

namespace ThucTapNhom.Controllers.API
{
    public class OrdersController : ApiController
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: api/Orders
        public IHttpActionResult GetOrders(
            int page = 1,
            int size = 10,
            int? status = null,
            string name = null,
            string phone = null,
            string startDate = null,
            string endDate = null,
            int? minPrice = null,
            int? maxPrice = null
        )
        {
            var orders = db.Orders.AsQueryable();
            if (status != null)
            {
                orders = orders.Where(s => s.Status == status);
            }
            if (name != null)
            {
                orders = orders.Where(s => s.CustomerName.Contains(name));
            } 
            if (phone != null)
            {
                orders = orders.Where(s => s.CustomerPhone.Equals(phone));
            }
            if (startDate != null)
            {
                var startDateFormat = Convert.ToDateTime(startDate);
                orders = orders.Where(s => s.CreatedAt >= startDateFormat);
            }
            if (endDate != null)
            {
                var tomorrow = Convert.ToDateTime(endDate).AddDays(1);
                orders = orders.Where(s => s.CreatedAt < tomorrow);
            }
            if (minPrice != null)
            {
                orders = orders.Where(s => s.TotalPrice >= minPrice);
            }
            if (maxPrice != null)
            {
                orders = orders.Where(s => s.TotalPrice >= maxPrice);
            }
            var skip = (page - 1) * size;

            var total = orders.Count();

            orders = orders
                .OrderByDescending(c=>c.CreatedAt)
                .Skip(skip)
                .Take(size);

            return Ok(new PagedResult<Order>(orders.ToList(), page, size, total));
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            var orderDetail = db.OrderDetails.FirstOrDefault(o => o.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(orderDetail);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                order.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = 0;
            order.DeletedAt = DateTime.Now;
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}