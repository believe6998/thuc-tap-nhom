using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThucTapNhom.Models
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext()
            : base("MyConnectionString")
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}