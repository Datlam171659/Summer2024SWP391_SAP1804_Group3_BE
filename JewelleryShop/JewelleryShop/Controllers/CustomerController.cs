using Microsoft.AspNetCore.Mvc;
using JewelleryShop.DataAccess.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using JewelleryShop.DataAccess.Models.ViewModel.CustomerViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess;

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
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
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
        public async Task<IActionResult> GetCustomersById(string id)
        {
            var CustomerById = await _context.Customers.FindAsync(id);
            if (CustomerById == null)
            {
                return NotFound();
            }
            return Ok(CustomerById);
        }

        private string GenerateCustomerId(string name, DateTime creationDate)
        {
            var initials = string.Join("", name.Split(' ').Take(3).Select(x => x[0]).ToArray()).ToUpper();
            var formattedDate = creationDate.ToString("ddMMyyHHmmss");
            return $"{initials}{formattedDate}";
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerInputDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Id = GenerateCustomerId(customerDto.CustomerName, DateTime.Now);
            customer.Status = "active";
            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, APIResponse<object>.ErrorResponse(new List<string> { ex.Message }, "An error occurred while saving the customer."));
            }

            var savedCustomerDto = _mapper.Map<CustomerCommonDTO>(customer);
            return CreatedAtAction("GetCustomersById", new { id = savedCustomerDto.Id }, APIResponse<CustomerCommonDTO>.SuccessResponse(savedCustomerDto, "Customer created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerById(string id, [FromBody] CustomerInputDTO newCustomerData)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
                return NotFound();

            _mapper.Map(newCustomerData, existingCustomer);

            _context.Customers.Update(existingCustomer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("A concurrency error occurred while saving changes: " + ex.Message);
            }

            return NoContent(); // Success
        }
    }
}