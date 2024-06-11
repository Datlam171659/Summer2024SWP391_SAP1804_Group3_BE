﻿using System;
using System.Collections.Generic;

namespace JewelleryShop.DataAccess.Models
{
    public partial class CustomerPromotion
    {
        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public decimal? DiscountPct { get; set; }
        public string? Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
