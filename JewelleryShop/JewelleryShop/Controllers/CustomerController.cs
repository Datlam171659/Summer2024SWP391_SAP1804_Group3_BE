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

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetCustomersByEmail(string email)
        {
            try
            {
                var customerByEmail = await _customerService.GetByEmailAsync(email);
                if (customerByEmail == null)
                {
                    return NotFound();
                }
                return Ok(customerByEmail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, APIResponse<object>.ErrorResponse(new List<string> { ex.Message }, "An error occurred while retrieving the customer."));
            }
        }

        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetCustomersByPhoneNumber(string phone)
        {
            try
            {
                var customerByPhone = await _customerService.GetByPhoneNumberAsync(phone);
                if (customerByPhone == null)
                {
                    return NotFound();
                }
                return Ok(customerByPhone);
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
            var updatedCustomerDto = await _customerService.UpdateCustomerAsync(id, newCustomerData);

            if (updatedCustomerDto == null)
            {
                return NotFound();
            }

            return Ok(updatedCustomerDto); // Success
        }
    }
}