﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } //Default status is "Pending"
    }
}
