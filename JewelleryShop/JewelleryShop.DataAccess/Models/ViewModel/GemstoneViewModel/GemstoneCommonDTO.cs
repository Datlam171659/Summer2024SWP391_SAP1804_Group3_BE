﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.GemstoneViewModel
{
    public class GemstoneCommonDTO
    {
        public string Id { get; set; } = null!;
        public string? GemstoneName { get; set; }
        public string? Colour { get; set; }
        public string? Rarity { get; set; }
        public string? Origin { get; set; }
        public int? Price { get; set; }
        public string? Hardness { get; set; }
        public string? Description { get; set; }
    }
}
