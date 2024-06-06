using BCrypt.Net;
using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.StaffViewModel;
using JewelleryShop.DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public EmployeeController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(StaffLoginDTO employee)
        {
            try
            {
                string token = await _staffService.LoginAsync(employee);

                return Ok(
                    APIResponse<string>
                    .SuccessResponse(token, "Login successully.")
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, APIResponse<object>.ErrorResponse(new List<string> { ex.Message }, "An error occurred while logging in."));
            }
        } 

        [HttpPost("Register")]
        public async Task<IActionResult> Register(StaffRegisterDTO employee)
        {
            try
            {
                var emp = await _staffService.AddEmployeeAsync(employee);

                return Ok(
                    APIResponse<StaffCommonDTO>
                    .SuccessResponse(emp, "Employee registered successully.")
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, 
                    APIResponse<object>.ErrorResponse(
                        new List<string> { ex.Message }, "An error occurred while registering employee.")
                    );
            }
        } 
    }
}
