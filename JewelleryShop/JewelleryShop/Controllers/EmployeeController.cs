using BCrypt.Net;
using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.StaffViewModel;
using JewelleryShop.DataAccess.Repository.Interface;
using JewelleryShop.DataAccess.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JewelleryShop.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IStaffService staffService, IUnitOfWork unitOfWork)
        {
            _staffService = staffService;
            _unitOfWork = unitOfWork; 
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("Staff")]
        public async Task<ActionResult<IEnumerable<StaffCommonDTO>>> GetStaff()
        {
            var staff = await _staffService.GetAllStaff();
            return Ok(staff);
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("Staff/{id}")]
        public async Task<IActionResult> GetStaffByID(string id)
        {
            var staff = await _staffService.GetStaffById(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        [AllowAnonymous]
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
        //[HttpPost("HashPassword")]
        //public async Task<IActionResult> HashPassword(string password)
        //{
        //    try
        //    {
        //        string hash = StringUtils.HashPassword(password);
        //        return Ok(
        //                APIResponse<string>
        //                .SuccessResponse(hash, "Login successully.")
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, APIResponse<object>.ErrorResponse(new List<string> { ex.Message }, "An error occurred while logging in."));
        //    }
        //} 

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(StaffRegisterDTO employee)
        {
            try
            {
                var emp = await _staffService.AddEmployeeAsync(employee);

                return Ok(
                    APIResponse<StaffCommonDTO>
                    .SuccessResponse(emp, "Employee Added successully.")
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, 
                    APIResponse<object>.ErrorResponse(
                        new List<string> { ex.Message }, "An error occurred while adding employee.")
                    );
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("DisableAccount/{id}")]
        public async Task<IActionResult> DisableAccount(string id)
        {
            var staff = await _unitOfWork.StaffRepository.GetByIdAsync(id);
            _unitOfWork.StaffRepository.DisableAccount(staff);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Disable Successfully."));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Staff/{id}")]        
        public async Task<IActionResult> UpdateStaff(string id, StaffRegisterDTO staff)
        {
            try
            {
                var updatedEmp = await _staffService.UpdateEmployeeAsync(id, staff);
                return Ok(APIResponse<StaffCommonDTO>.SuccessResponse(updatedEmp, "Employee updated successfully."));
            } catch (Exception ex) {
                return StatusCode(500, 
                    APIResponse<object>.ErrorResponse(
                        new List<string> { ex.Message }, "An error occurred while updating the employee."));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Staff/{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            try
            {
                await _staffService.DeleteEmployeeAsync(id);
                return Ok(APIResponse<string>.SuccessResponse(id, "Employee deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    APIResponse<object>.ErrorResponse(
                        new List<string> { ex.Message }, "An error occurred while deleting the employee."));
            }
        }
    }
}
