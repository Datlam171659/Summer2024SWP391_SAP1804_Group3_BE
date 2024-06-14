using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
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
        private readonly IUnitOfWork _unitOfWork;

        public DiscountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscountList()
        {
            var list = _unitOfWork.DiscountRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscount(int id)
        {
            var discount = await _unitOfWork.DiscountRepository.GetByIdAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return Ok(discount);
        }

        [HttpPost]
        public async Task<ActionResult<Discount>> CreateDiscount(Discount request)
        {
            _unitOfWork.DiscountRepository.AddAsync(request);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Create successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id)
        {
            var discount = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
            _unitOfWork.DiscountRepository.Update(discount);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
            _unitOfWork.DiscountRepository.Remove(discount);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Delete successfully."));
        }

     
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveDiscount(int id)
        {
            var discount = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
            _unitOfWork.DiscountRepository.Approve(discount);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Approve successfully."));
        }

  
        [HttpPut("request/{id}")]
        public async Task<IActionResult> RequestDiscount(int id)
        {
            var discount = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
            _unitOfWork.DiscountRepository.Request(discount);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Request successfully."));
        }
    }
}
