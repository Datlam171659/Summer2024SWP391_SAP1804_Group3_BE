using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Role
    {
        public Role()
        {
            staff = new HashSet<staff>();
        }

        [Key]
        public int RoleId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
