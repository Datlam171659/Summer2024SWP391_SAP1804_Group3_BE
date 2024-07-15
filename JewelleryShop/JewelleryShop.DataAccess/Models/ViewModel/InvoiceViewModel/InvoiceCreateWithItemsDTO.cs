using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel
{
    public class InvoiceCreateWithItemsDTO
    {
        public InvoiceInputNewDTO invoice { get; set; } = null!;
        public IEnumerable<InvoiceInputItemDTO>? items { get; set; }
    }
}
