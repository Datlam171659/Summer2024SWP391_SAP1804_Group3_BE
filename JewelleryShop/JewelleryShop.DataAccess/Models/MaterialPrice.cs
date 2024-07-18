using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class MaterialPrice
    {
        [Key]
        public string Id { get; set; } = null!;
        public string? Symbol { get; set; }
        public decimal? PriceUsd { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
