using Microsoft.AspNetCore.Mvc;
using JewelleryShop.DataAccess.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using JewelleryShop.DataAccess.Models.ViewModel.CustomerViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess;
using JewelleryShop.Business.Service.Interface;

namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly JewelleryDBContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(JewelleryDBContext context, IMapper mapper, ICustomerService customerService)
        {
            _context = context;
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var data = await _customerService.GetAllAsync();

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
            try
            {
                var customerById = await _customerService.GetByIDAsync(id);

                if (customerById == null)
                {
                    return NotFound();
                }

                return Ok(customerById);
            }
            catch (Exception ex)
            {
                return StatusCode(500, APIResponse<object>.ErrorResponse(new List<string> { ex.Message }, "An error occurred while retrieving the customer."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerInputDTO customerDto)
        {
            var customer = await _customerService.CreateCustomerAsync(customerDto);
            if (customer == null)
            {
                return StatusCode(500, APIResponse<object>.ErrorResponse(new List<string>(), "An error occurred while creating the customer."));
            }
            return CreatedAtAction("GetCustomersById", new { id = customer.Id }, customer);
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