using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Brand
    {
        [Key]
        public string Id { get; set; } = null!;
        public string? ItemId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual Item? Item { get; set; }
    }
}
