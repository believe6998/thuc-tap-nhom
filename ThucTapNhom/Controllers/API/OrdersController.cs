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
using Newtonsoft.Json.Linq;
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
                .OrderByDescending(c => c.CreatedAt)
                .Skip(skip)
                .Take(size);

            return Ok(new PagedResult<Order>(orders.ToList(), page, size, total));
        }

        // GET: api/Orders/5
        public IHttpActionResult GetOrder(int id)
        {
            
           var orderDetail = from o in db.OrderDetails
                join p in db.Products on o.ProductId equals p.Id
                where o.OrderId == id
                select new
                {
                    Img = p.ImageUrl,
                    Name = p.Name,
                    Quantity = o.Quantity,
                    Price = p.Price,
                    UnitPrice = p.Price*o.Quantity
                };

           return Ok(orderDetail.ToList());
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
        public IHttpActionResult PostOrder(JObject data)
        {
            dynamic jsonData = data;
            var customerName = jsonData.customerName;
            var customerPhone = jsonData.customerPhone;
            var totalPrice = jsonData.totalPrice;
            JArray products = jsonData.products;
            var order = new Order
            {
                CustomerName = customerName,
                CustomerPhone = customerPhone,
                TotalPrice = totalPrice
            };
            db.Orders.Add(order);
            db.SaveChanges();
            for (var i = 0; i < products.Count; i++)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = (int) products[i]["id"],
                    Quantity = (int) products[i]["quantity"]
                };
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            return StatusCode(HttpStatusCode.NoContent);
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