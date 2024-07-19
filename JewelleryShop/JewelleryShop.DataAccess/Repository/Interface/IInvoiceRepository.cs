using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.ItemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository.Interface
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        public Task<InvoiceCWIReturnDTO> CreateInvoiceWithItemsAsync(Invoice invoice, IEnumerable<InvoiceInputItemDTO> items);
        public Task<InvoiceBBWIReturnDTO> CreateBuyBackInvoiceWithItemsAsync(Invoice invoice, IEnumerable<InvoiceBuyBackInputItemDTO> items);
        public Task<List<ItemInvoice>> GetInvoiceItems(string invoiceID);
        public Task<List<Invoice>> GetAllCustomerInvoice(string customerID);
        public Task<Invoice> GetInvoiceByInvoiceNumber(string invoiceNumber);
    }
}
