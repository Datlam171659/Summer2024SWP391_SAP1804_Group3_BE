using Microsoft.AspNetCore.Mvc;
using JewelleryShop.DataAccess.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;
namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase 
    {
        private readonly JewelleryDBContext _context;
        private readonly IMapper _mapper;

        public SalesController(JewelleryDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        // Invoice APIs
        [HttpGet("Invoices")]
        public async Task<ActionResult<IEnumerable<InvoiceCommonDTO>>> GetInvoice()
        {
            return _mapper.Map<List<InvoiceCommonDTO>>(await _context.Invoices.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoiceById(string id)
        {
            var InvoiceById = await _context.Invoices.FindAsync(id);
            if (InvoiceById == null)
            {
                return NotFound();
            }
            return Ok(InvoiceById);
        }
        [HttpGet("Warranty")]
        public async Task<ActionResult<IEnumerable<WarrantyCommonDTO>>> GetWarranty()
        {
            return _mapper.Map<List<WarrantyCommonDTO>>(await _context.Warranties.ToListAsync());
        }

        [HttpGet("Warranty/{id}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetWarrantyById(string id)
        {
            var WarrantyById = await _context.Warranties.FindAsync(id);
            if (WarrantyById == null)
            {
                return NotFound();
            }
            return Ok(WarrantyById);
        }
    }
}
