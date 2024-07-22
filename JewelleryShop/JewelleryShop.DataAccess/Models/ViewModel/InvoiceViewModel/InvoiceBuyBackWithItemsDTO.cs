using JewelleryShop.DataAccess.Models.ViewModel.ItemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel
{
    public class InvoiceBuyBackWithItemsDTO
    {
        public InvoiceInputNewDTO invoice { get; set; } = null!;
        public IEnumerable<InvoiceBuyBackInputItemDTO>? items { get; set; }
    }
}
