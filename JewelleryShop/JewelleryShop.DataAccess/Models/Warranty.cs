using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Warranty
    {
        public Warranty()
        {
            ItemInvoices = new HashSet<ItemInvoice>();
        }

        [Key]
        public string WarrantyId { get; set; } = null!;

        [Required]
        public string CustomerId { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<ItemInvoice> ItemInvoices { get; set; }
    }
}
