using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel
{
    public class InvoiceCreateWithItemsDTO
    {
        public InvoiceInputNewDTO invoiceDTO { get; set; } = null!;
        public IEnumerable<string> itemIds { get; set; }
        public string returnPolicyId { get; set; }
        public string warrantyId { get; set; }
    }
}
