using System;
using System.Collections.Generic;

namespace JewelleryShop.DataAccess.Models
{
    public partial class ReturnPolicy
    {
        public string Id { get; set; } = null!;
        public string? ReturnPolicyType { get; set; }
        public int? ReturnDuration { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
