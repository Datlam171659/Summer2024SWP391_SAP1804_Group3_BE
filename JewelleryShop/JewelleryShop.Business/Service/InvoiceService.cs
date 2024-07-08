using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceItemsViewModel;

public class InvoiceService : IInvoiceService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<List<InvoiceCommonDTO>> GetAllInvoices()
    {
        var invoices = await _unitOfWork.InvoiceRepository.GetAllAsync();
        return _mapper.Map<List<InvoiceCommonDTO>>(invoices);
    }

    public async Task<InvoiceCommonDTO> GetInvoiceById(string id)
    {
        var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(id);
        return _mapper.Map<InvoiceCommonDTO>(invoice);
    }

    public async Task<InvoiceCommonDTO> AddInvoice(InvoiceInputDTO invoiceDTO)
    {
        var invoice = _mapper.Map<Invoice>(invoiceDTO);
        invoice.Id = Guid.NewGuid().ToString();
        await _unitOfWork.InvoiceRepository.AddAsync(invoice);
        await _unitOfWork.SaveChangeAsync();

        return _mapper.Map<InvoiceCommonDTO>(invoice);
    }

    public async Task<InvoiceCreateWithItemsDTO> CreateInvoiceWithItemsAsync(InvoiceInputNewDTO invoiceDTO, IEnumerable<InvoiceInputItemDTO> items)
    {
        var invoice = _mapper.Map<Invoice>(invoiceDTO);
        var appNameShort = _configuration.GetValue<string>("Settings:AppNameShort");

        var invoiceNoExist = await _unitOfWork.InvoiceRepository.GetInvoiceByInvoiceNumber(invoice.InvoiceNumber);
        if (invoiceNoExist != null) throw new Exception("Duplicate Invoice Number.");

        List<Invoice> _customerInvoiceNo = await _unitOfWork.InvoiceRepository.GetAllCustomerInvoice(invoice.CustomerId);
        int customerInvoiceNo = _customerInvoiceNo.Count;
        Interlocked.Add(ref customerInvoiceNo, 1); // 4 safety

        invoice.Id = Guid.NewGuid().ToString();
        invoice.InvoiceNumber = invoice.InvoiceNumber.IsNullOrEmpty() ? $"{appNameShort}-{invoice.CustomerId}-{DateTime.Now.ToString("ddMMyyHH")}-{customerInvoiceNo}" : invoice.InvoiceNumber;
        var res = await _unitOfWork.InvoiceRepository.CreateInvoiceWithItemsAsync(invoice, items);
        await _unitOfWork.SaveChangeAsync();

        return res;
    }

    public async Task<List<ItemInvoiceCommonDTO>> GetInvoiceItems(string invoiceID)
    {
        var res = await _unitOfWork.InvoiceRepository.GetInvoiceItems(invoiceID);
        return _mapper.Map<List<ItemInvoiceCommonDTO>>(res);
    }

    public async Task<List<InvoiceCommonDTO>> GetAllCustomerInvoice(string customerID)
    {
        var res = await _unitOfWork.InvoiceRepository.GetAllCustomerInvoice(customerID);
        return _mapper.Map<List<InvoiceCommonDTO>>(res);
    }

    public async Task<List<KeyValuePair<string, decimal>>> GetMonthlyRevenue()
    {
        var invoices = await _unitOfWork.InvoiceRepository.GetAllAsync();
        var validInvoices = invoices.Where(i => i.CreatedDate.HasValue);
        var monthlyRevenue = validInvoices
            .GroupBy(i => i.CreatedDate.Value.ToString("yyyy-MM"))
            .OrderBy(g => g.Key)
            .Select(g => new KeyValuePair<string, decimal>(
                g.Key,
                g.Sum(i => i.SubTotal ?? 0)))
            .ToList();

        return monthlyRevenue;
    }

    public async Task<InvoiceCommonDTO> GetInvoiceByInvoiceNumber(string invoiceNumber)
    {
        var res = await _unitOfWork.InvoiceRepository.GetInvoiceByInvoiceNumber(invoiceNumber);
        return _mapper.Map<InvoiceCommonDTO>(res);
    }
}