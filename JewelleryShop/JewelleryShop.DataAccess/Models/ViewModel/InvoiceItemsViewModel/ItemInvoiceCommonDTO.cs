using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.ItemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceItemsViewModel
{
    public class ItemInvoiceCommonDTO
    {
        public string ItemId { get; set; } = null!;
        public string InvoiceId { get; set; } = null!;
        public string ReturnPolicyId { get; set; } = null!;
        public string WarrantyId { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public virtual ItemDTO Item { get; set; } = null!;
        public virtual InvoiceCommonDTO Invoice { get; set; } = null!;
    }
}
