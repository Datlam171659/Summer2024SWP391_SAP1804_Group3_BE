﻿using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly JewelleryDBContext _context;

        public DiscountController(JewelleryDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        [HttpPost]
        public async Task<ActionResult<Discount>> CreateDiscount(DiscountDto request)
        {
            var discount = new Discount
            {
                DiscountCode = request.DiscountCode,
                DiscountPercentage = request.DiscountPercentage,
                Description = request.Description,
                Status = "Pending"  // Set initial status to "Pending"
            };

            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDiscount), new { id = discount.DiscountId }, discount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, DiscountDto request)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            discount.DiscountCode = request.DiscountCode;
            discount.DiscountPercentage = request.DiscountPercentage;
            discount.Description = request.Description;

            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

     
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            discount.Status = "Approved"; // Update status to "Approved"

            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

  
        [HttpPut("request/{id}")]
        public async Task<IActionResult> RequestDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            discount.Status = "Pending"; // Update status to "Pending"

            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
