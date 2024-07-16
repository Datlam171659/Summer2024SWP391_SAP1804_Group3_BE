using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel
{
    public class InvoiceCWIReturnDTO
    {
        public InvoiceCommonDTO invoice { get; set; } = null!;
        public IEnumerable<InvoiceInputItemDTO>? items { get; set; }
    }
}
