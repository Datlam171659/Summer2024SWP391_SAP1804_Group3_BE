using Microsoft.AspNetCore.Mvc;
using JewelleryShop.DataAccess.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelleryShop.DataAccess.Models.dto;
using AutoMapper;
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
            _context = context;
            _mapper = mapper;
        }
        // Invoice APIs
        [HttpGet("Invoices")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoice()
        {
            return await _context.Invoices.ToListAsync();
        }

        [HttpGet("Invoice/{id}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoiceById(string id)
        {
            var InvoiceById = await _context.Invoices.FindAsync(id);
            if (InvoiceById == null)
            {
                return NotFound();
            }
            return Ok(InvoiceById);
        }


        //WarrantyAPI
        [HttpGet("Warranty")]
        public async Task<ActionResult<IEnumerable<Warranty>>> GetWarranty()
        {
            return await _context.Warranties.ToListAsync();
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

        [HttpPost("Invoices")]
        public async Task<ActionResult<InvoiceDTO>> PostInvoice([FromBody] InvoiceDTO invoice)
        {
            _context.Invoices.Add(_mapper.Map<Invoice>(invoice));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        [HttpPost("Warranties")]
        public async Task<ActionResult<Warranty>> PostWarranty([FromBody] Warranty warranty)
        {
            _context.Warranties.Add(warranty);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetWarranty), new { id = warranty.WarrantyId }, warranty);
        }
    }
}
