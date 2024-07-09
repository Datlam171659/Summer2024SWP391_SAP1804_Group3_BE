﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel
{
    public class InvoiceInputItemDTO
    {
        public string itemID { get; set; }
        public string ReturnPolicyID { get; set; }
        public int itemQuantity { get; set; }
        public DateTime warrantyExpiryDate { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

    }
}
