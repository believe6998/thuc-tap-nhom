﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThucTapNhom.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
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
    }
}