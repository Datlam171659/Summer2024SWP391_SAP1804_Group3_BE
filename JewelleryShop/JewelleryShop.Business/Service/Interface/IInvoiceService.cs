using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service.Interface
{
    public interface IInvoiceService
    {
        Task<List<InvoiceCommonDTO>> GetAllInvoices();
        Task<InvoiceCommonDTO> GetInvoiceById(string id);
        Task<InvoiceCommonDTO> AddInvoice(InvoiceInputDTO invoiceDTO);
        Task<InvoiceWithItemsDTO> CreateInvoiceWithItemsAsync(InvoiceInputNewDTO invoiceDTO, IEnumerable<InvoiceInputItemDTO> items, string returnPolicyId, string warrantyId);
        Task<List<Item>> GetInvoiceItems(string invoiceID);
    }
}
