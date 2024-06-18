﻿using System;
using System.Collections.Generic;

namespace JewelleryShop.DataAccess.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
            RewardsPrograms = new HashSet<RewardsProgram>();
            Warranties = new HashSet<Warranty>();
        }

        public string Id { get; set; } = null!;
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<RewardsProgram> RewardsPrograms { get; set; }
        public virtual ICollection<Warranty> Warranties { get; set; }
        public virtual ICollection<CustomerPromotion> Promotion { get; set; }
    }
}
