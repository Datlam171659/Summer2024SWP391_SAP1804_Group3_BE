using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    public class CustomerPromotionController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerPromotionController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePromotion(CustomerPromotion promotion)
        {
            _unitOfWork.CustomerPromotionRepository.AddAsync(promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Create successfully."));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotion(string id)
        {
            var promotion = await _unitOfWork.CustomerPromotionRepository.GetByIdAsync(id);
            _unitOfWork.CustomerPromotionRepository.Update(promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update Successfully."));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(string id) 
        {
            var promotion = await _unitOfWork.CustomerPromotionRepository.GetByIdAsync(id);
            _unitOfWork.CustomerPromotionRepository.Remove(promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Delete Successfully."));
        }

    }
}
