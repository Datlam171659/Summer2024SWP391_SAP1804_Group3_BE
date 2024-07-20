using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class ItemInvoice
    {
        [Required]
        public string ItemId { get; set; } = null!;

        [Required]
        public string InvoiceId { get; set; } = null!;
        public string ReturnPolicyId { get; set; } = null!;
        public string WarrantyId { get; set; } = null!;
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }

        public virtual Invoice Invoice { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
        public virtual ReturnPolicy ReturnPolicy { get; set; } = null!;
        public virtual Warranty Warranty { get; set; } = null!;
    }
}
