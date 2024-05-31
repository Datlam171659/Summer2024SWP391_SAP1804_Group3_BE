using Microsoft.AspNetCore.Mvc;
using JewelleryShop.DataAccess.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using JewelleryShop.DataAccess.Models.dto;
using AutoMapper;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using Newtonsoft.Json.Linq;

using AutoMapper;
using JewelleryShop.DataAccess.Models.ViewModel.CustomerViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;


namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly JewelleryDBContext _context;
        private readonly IMapper _mapper;

        public CustomerController(JewelleryDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<List<CustomerDTO>>> GetCustomers()
        {
            return Ok(
                    APIResponse<List<CustomerDTO>>
                    .SuccessResponse(
                        _mapper.Map<List<CustomerDTO>>(await _context.Customers.ToListAsync()), 
                        "Get successully.")
                );
             

        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var data = _mapper.Map<List<CustomerCommonDTO>>(await _context.Customers.ToListAsync());

                return Ok(
                    APIResponse<List<CustomerCommonDTO>>
                    .SuccessResponse(data, "Login successully.")
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, APIResponse<object>.ErrorResponse(new List<string> { ex.Message }, "An error occurred while logging in."));
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersById(string id)
        {
            var CustomerById = await _context.Customers.FindAsync(id);
            if (CustomerById == null)
            {
                return NotFound();
            }
            return Ok(CustomerById);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customer)
        {
            try
            {
                _context.Customers.Add(
                    _mapper.Map<Customer>(customer)
                    );
                await _context.SaveChangesAsync();
                return Ok(
                    APIResponse<string>
                    .SuccessResponse(
                        "success",
                        "Get successully.")
                );

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerById(string id, [FromBody] Customer newCustomerData)
        {
            if (newCustomerData == null || !newCustomerData.Id.Equals(id))
                return BadRequest();

            var existingCustomer = await _context.Customers.FindAsync(id);
            
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.CustomerName = newCustomerData.CustomerName;
            existingCustomer.Address = newCustomerData.Address;
            existingCustomer.Gender = newCustomerData.Gender;
            existingCustomer.PhoneNumber = newCustomerData.PhoneNumber;
            existingCustomer.Email = newCustomerData.Email;
            existingCustomer.Status = newCustomerData.Status;

            _context.Customers.Update(existingCustomer);
            try { 
            await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("A concurrency error occurred while saving changes: " + ex.Message);
            }

            return NoContent(); // Success
        }
    }
}