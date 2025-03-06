using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelleryShop.DataAccess.Models
{
    public partial class PriceMultiplier
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal BuyMultiplier { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal SellMultiplier { get; set; }
    }
}
