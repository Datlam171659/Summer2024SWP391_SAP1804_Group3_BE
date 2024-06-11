﻿using Microsoft.AspNetCore.Mvc;
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
namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase 
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceService _invoiceService;
        private readonly IWarrantyService _warrantyService; 


        public SalesController(JewelleryDBContext context, IMapper mapper, IInvoiceService invoiceService, IWarrantyService warrantyService)
        {
            _mapper = mapper;
            _invoiceService = invoiceService;
            _warrantyService = warrantyService;
        }
        // Invoice APIs
        [HttpGet("Invoices")]
        public async Task<ActionResult<IEnumerable<InvoiceCommonDTO>>> GetInvoice()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }
        
        [HttpPost("CreateInvoiceWithItems")]
        public async Task<ActionResult> CreateInvoiceWithItemsAsync(InvoiceCreateWithItemsDTO data)
        {
            var res = await _invoiceService.CreateInvoiceWithItemsAsync(data.invoiceDTO, data.itemIds, data.returnPolicyId, data.warrantyId);
            return Ok(
                APIResponse<InvoiceWithItemsDTO>.SuccessResponse(
                    data: res, 
                    message:"Successfully created invoice."
                )
            );
        }

        [HttpGet("InvoiceItems")]
        public async Task<ActionResult<IEnumerable<InvoiceCommonDTO>>> GetInvoiceItems(string invoiceID)
        {
            var invoices = await _invoiceService.GetInvoiceItems(invoiceID);
            return Ok(invoices);
        }

        [HttpGet("Invoice/{id}")]
        public async Task<ActionResult<IEnumerable<InvoiceCommonDTO>>> GetInvoiceById(string id)
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

        [HttpPost("Invoices")]
        public async Task<ActionResult<InvoiceCommonDTO>> PostInvoice([FromBody] InvoiceInputDTO invoiceDTO)
        {
            var createdInvoiceDTO = await _invoiceService.AddInvoice(invoiceDTO);
            return CreatedAtAction(nameof(GetInvoice), new { id = createdInvoiceDTO.Id }, createdInvoiceDTO);
        }

        [HttpPost("Warranties")]
        public async Task<ActionResult<WarrantyCommonDTO>> PostWarranty([FromBody] WarrantyInputDTO warrantyDTO)
        {
            var createdWarrantyDTO = await _warrantyService.AddWarranty(warrantyDTO);
            return CreatedAtAction(nameof(GetWarrantyById), new { id = createdWarrantyDTO.WarrantyId }, createdWarrantyDTO);
        }
    }
}
