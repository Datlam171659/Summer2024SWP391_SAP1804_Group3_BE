using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class RewardsProgram
    {
        [Key]
        public string Id { get; set; } = null!;
        public string? CustomerId { get; set; }
        public int? PointsTotal { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
