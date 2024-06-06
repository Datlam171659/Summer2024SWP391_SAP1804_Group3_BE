using System;
using System.Collections.Generic;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Warranty
    {
        public string WarrantyId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
