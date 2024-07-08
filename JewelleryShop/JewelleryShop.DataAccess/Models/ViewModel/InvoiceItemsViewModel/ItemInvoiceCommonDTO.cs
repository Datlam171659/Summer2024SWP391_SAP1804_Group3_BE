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
        public int? Price { get; set; }

        public ItemDTO Item { get; set; } = null!;
    }
}
