using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class ItemImage
    {
        [Key]
        public string Id { get; set; } = null!;
        public string? ItemId { get; set; }
        public string? ImageUrl { get; set; }
        public string? ThumbnailUrl { get; set; }

        public virtual Item? Item { get; set; }
    }
}
