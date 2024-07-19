using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Image
    {
        public Image()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        public string ItemImagesId { get; set; } = null!;
        public string ImageSource { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
