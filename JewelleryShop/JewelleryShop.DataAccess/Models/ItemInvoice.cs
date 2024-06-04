using System;
using System.Collections.Generic;

namespace JewelleryShop.DataAccess.Models
{
    public partial class ItemInvoice
    {
        public string? ItemId { get; set; }
        public string? InvoiceId { get; set; }
        public string? ReturnPolicyId { get; set; }
        public string? WarrantyId { get; set; }

        public virtual Invoice? Invoice { get; set; }
        public virtual Item? Item { get; set; }
        public virtual ReturnPolicy? ReturnPolicy { get; set; }
        public virtual Warranty? Warranty { get; set; }
    }
}
