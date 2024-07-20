using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        public string DiscountCode { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
