using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class StaffStation
    {
        public StaffStation()
        {
            staff = new HashSet<staff>();
        }

        [Required]
        public string StationId { get; set; } = null!;
        public string StaffId { get; set; } = null!;
        public string? StaionName { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
