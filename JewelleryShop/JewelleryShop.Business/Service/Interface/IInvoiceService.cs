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
        public Task<List<InvoiceCommonDTO>> GetAllInvoices();
        public Task<InvoiceCommonDTO> GetInvoiceById(string id);
        public Task<InvoiceCommonDTO> AddInvoice(InvoiceInputDTO invoiceDTO);
    }
}
