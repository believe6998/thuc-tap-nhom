using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThucTapNhom.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt
        {
            get =>
                _dateCreated ?? DateTime.Now;

            set => this._dateCreated = value;
        }

        private DateTime? _dateCreated = null;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int Status { get; set; }
        public int CategoryId { get; set; } = 1;
    }
}