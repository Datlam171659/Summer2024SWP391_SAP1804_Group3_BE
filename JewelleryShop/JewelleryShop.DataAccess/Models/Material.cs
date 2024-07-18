using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Material
    {
        [Key]
        public string MaterialId { get; set; } = null!;
        public string? MaterialName { get; set; }
        public string? MaterialDescription { get; set; }
    }
}
