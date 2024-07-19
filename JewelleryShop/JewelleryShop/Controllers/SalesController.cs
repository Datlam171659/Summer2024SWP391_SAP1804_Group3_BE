using Microsoft.AspNetCore.Mvc;
using JewelleryShop.DataAccess.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelleryShop.DataAccess.Models.dto;
using AutoMapper;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.Business.Service;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.ItemImageViewModel;
using Microsoft.IdentityModel.Tokens;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceItemsViewModel;
namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase 
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IWarrantyService _warrantyService; 


        public SalesController(IInvoiceService invoiceService, IWarrantyService warrantyService)
        {
            _invoiceService = invoiceService;
            _warrantyService = warrantyService;
        }

        [HttpGet("Invoices")]
        public async Task<ActionResult<IEnumerable<InvoiceCommonDTO>>> GetInvoice()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }
        
        [HttpPost("CreateInvoiceWithItems")]
        public async Task<IActionResult> CreateInvoiceWithItemsAsync(InvoiceCreateWithItemsDTO data)
        {
            try
            {
                var res = await _invoiceService.CreateInvoiceWithItemsAsync(data.invoice, data.items);
                return Ok(
                    APIResponse<InvoiceCWIReturnDTO>.SuccessResponse(
                        data: res,
                        message: "Successfully created invoice."
                    )
                );
            }
            catch (Exception ex)
            {
                var response = APIResponse<string>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }

        }

        [HttpPost("CreateBuyBackInvoiceWithItems")]
        public async Task<IActionResult> CreateBuyBackInvoiceWithItemsAsync(InvoiceBuyBackWithItemsDTO data)
        {
            try
            {
                var res = await _invoiceService.CreateBuyBackInvoiceWithItemsAsync(data.invoice, data.items);
                return Ok(
                    APIResponse<InvoiceBBWIReturnDTO>.SuccessResponse(
                        data: res,
                        message: "Successfully created buyback invoice."
                    )
                );
            }
            catch (Exception ex)
            {
                var response = APIResponse<string>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }

        }

        [HttpGet("InvoiceItems/{invoiceID}")]
        public async Task<IActionResult> GetInvoiceItems(string invoiceID)
        {
            try
            {
                var invoices = await _invoiceService.GetInvoiceItems(invoiceID);
                if (invoices.IsNullOrEmpty())
                {
                    var response = APIResponse<string>
                        .ErrorResponse(new List<string> { "No records found with the provided ID." });
                    return NotFound(response);
                }
                return Ok(
                    APIResponse<List<ItemInvoiceCommonDTO>>
                        .SuccessResponse(data: invoices, "Successfully fetched invoice Items.")
                    );
            }
            catch (Exception ex)
            {
                var response = APIResponse<string>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }
        
        [HttpGet("CustomerInvoice/{customerID}")]
        public async Task<IActionResult> GetAllCustomerInvoice(string customerID)
        {
            try
            {
                var invoices = await _invoiceService.GetAllCustomerInvoice(customerID);
                if (invoices.IsNullOrEmpty())
                {
                    var response = APIResponse<string>
                        .ErrorResponse(new List<string> { "No records found with the provided ID." });
                    return NotFound(response);
                }
                return Ok(
                    APIResponse<List<InvoiceCommonDTO>>
                        .SuccessResponse(data: invoices, "Successfully fetched customer Invoices.")
                    );
            }
            catch (Exception ex)
            {
                var response = APIResponse<string>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

        [HttpGet("Invoice/ByNumber/{invoiceNumber}")]
        public async Task<IActionResult> GetInvoiceByInvoiceNumber(string invoiceNumber)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceByInvoiceNumber(invoiceNumber);
                if (invoice == null)
                {
                    var response = APIResponse<string>
                        .ErrorResponse(new List<string> { "No records found with the provided ID." });
                    return NotFound(response);
                }
                return Ok(
                    APIResponse<InvoiceCommonDTO>
                        .SuccessResponse(data: invoice, "Successfully fetched Invoice.")
                    );
            }
            catch (Exception ex)
            {
                var response = APIResponse<string>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

        [HttpGet("Invoice/{id}")]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            var invoiceDTO = await _invoiceService.GetInvoiceById(id);
            if (invoiceDTO == null)
            {
                return NotFound();
            }
            return Ok(invoiceDTO);
        }


        [HttpGet("Warranty")]
        public async Task<ActionResult<IEnumerable<WarrantyCommonDTO>>> GetWarranty()
        {
            var warranties = await _warrantyService.GetAllWarranty(); 
            return Ok(warranties);
        }

        [HttpGet("Warranty/{id}")]
        public async Task<ActionResult<IEnumerable<WarrantyCommonDTO>>> GetWarrantyById(string id)
        {
            var warranty = await _warrantyService.GetWarrantyById(id);
            if (warranty == null)
            {
                return NotFound();
            }
            return Ok(warranty);
        }

        //[HttpPost("Invoices")]
        //public async Task<ActionResult<InvoiceCommonDTO>> PostInvoice([FromBody] InvoiceInputDTO invoiceDTO)
        //{
        //    var createdInvoiceDTO = await _invoiceService.AddInvoice(invoiceDTO);
        //    return CreatedAtAction(nameof(GetInvoice), new { id = createdInvoiceDTO.Id }, createdInvoiceDTO);
        //}

        [HttpPost("Warranties")]
        public async Task<ActionResult<WarrantyCommonDTO>> PostWarranty([FromBody] WarrantyInputDTO warrantyDTO)
        {
            var createdWarrantyDTO = await _warrantyService.AddWarranty(warrantyDTO);
            return CreatedAtAction(nameof(GetWarrantyById), new { id = createdWarrantyDTO.WarrantyId }, createdWarrantyDTO);
        }

        [HttpGet("MonthlyRevenue")]
        public async Task<ActionResult<List<KeyValuePair<string, decimal>>>> GetMonthlyRevenue()
        {
            var monthlyRevenue = await _invoiceService.GetMonthlyRevenue();
            return Ok(monthlyRevenue);
        }
    }
}
