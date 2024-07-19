using JewelleryShop.DataAccess.Models.ViewModel.ItemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel
{
    public class InvoiceBBWIReturnDTO
    {
        public InvoiceCommonDTO invoice { get; set; } = null!;
        public IEnumerable<ItemCreateDTO>? items { get; set; }
    }
}
