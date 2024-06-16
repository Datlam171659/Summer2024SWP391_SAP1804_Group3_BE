﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel
{
    public class InvoiceDetailsDTO
    {
        public string Id { get; set; } = null!;
        public string? StaffId { get; set; }
        public string? ReturnPolicyId { get; set; }
        public List<Item>? Items { get; set; }
        public string? CustomerId { get; set; }
        public string? CompanyName { get; set; }
        public string? BuyerAddress { get; set; }
        public string? Status { get; set; }
        public string? PaymentType { get; set; }
        public int? Quantity { get; set; }
        public decimal? SubTotal { get; set; }
    }
}
