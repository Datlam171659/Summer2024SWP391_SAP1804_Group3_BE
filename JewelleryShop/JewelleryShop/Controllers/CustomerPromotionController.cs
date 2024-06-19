using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.PromotionViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPromotionController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ICustomerPromotionService _customerPromotionService;

        public CustomerPromotionController(UnitOfWork unitOfWork, ICustomerPromotionService customerPromotionService)
        {
            _unitOfWork = unitOfWork;
            _customerPromotionService = customerPromotionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePromotion(CustomerPromotionDto promotion)
        {
            promotion.Status = "Chờ duyệt";
            _customerPromotionService.AddAsync(promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Create successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotion(string id)
        {
            var promotion = await _unitOfWork.CustomerPromotionRepository.GetByIdAsync(id);
            _customerPromotionService.Update(promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update Successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(string id) 
        {
            var promotion = await _unitOfWork.CustomerPromotionRepository.GetByIdAsync(id);
            _customerPromotionService.Delete(promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Delete Successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ApprovePromotion(int id)
        {
            var discount = await _unitOfWork.CustomerPromotionRepository.GetByIdAsync(id);
            _customerPromotionService.Approve(discount);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Approve successfully."));
        }

    }
}
