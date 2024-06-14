﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.PromotionViewModel
{
    public class CustomerPromotionDto
    {
        public string Id { get; set; } = null;
        public string Code { get; set; }
        public decimal  DiscountPct { get; set; }
        public string Status { get; set; }
        public DateTime ExpiryDate { get; set; }
                
    }
}
