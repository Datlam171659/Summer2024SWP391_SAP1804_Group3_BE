using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess;

public class InvoiceService : IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<InvoiceCommonDTO>> GetAllInvoices()
    {
        var invoices = await _unitOfWork.InvoiceRepository.GetAllAsync();
        return _mapper.Map<List<InvoiceCommonDTO>>(invoices);
    }

    public async Task<InvoiceCommonDTO> GetInvoiceById(string id)
    {
        var invoice = await _unitOfWork.InvoiceRepository.GetByInvoiceIdAsync(id);
        return _mapper.Map<InvoiceCommonDTO>(invoice);
    }

    public async Task<InvoiceCommonDTO> AddInvoice(InvoiceInputDTO invoiceDTO)
    {
        var invoice = _mapper.Map<Invoice>(invoiceDTO);
        invoice.Id = Guid.NewGuid().ToString();
        _unitOfWork.InvoiceRepository.Add(invoice);
        await _unitOfWork.SaveChangeAsync();

        return _mapper.Map<InvoiceCommonDTO>(invoice);
    }
}