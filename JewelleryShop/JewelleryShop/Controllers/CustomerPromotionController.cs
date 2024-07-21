
﻿using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.PromotionViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPromotionController : Controller
    {
        private readonly ICustomerPromotionService _customerPromotionService;

        public CustomerPromotionController(ICustomerPromotionService customerPromotionService)
        {
            _customerPromotionService = customerPromotionService;
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet]
        public async Task<IActionResult> ListPromotion()
        {
            var list = await _customerPromotionService.GetAll();
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPost]
        public async Task<IActionResult> CreatePromotion(CustomerPromotionDto promotion)
        {
            await _customerPromotionService.AddAsync(promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Create promotion successfully."));
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePromotion(string id, CustomerPromotionDto promotion)
        {
            await _customerPromotionService.Update(id, promotion);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update promotion Successfully."));
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(string id) 
        {
            await _customerPromotionService.Delete(id);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Delete Successfully."));
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApprovePromotion(string id)
        {
            await _customerPromotionService.Approve(id);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Approve successfully."));
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectPromotion(string id)
        {
            await _customerPromotionService.Reject(id);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Reject successfully."));
        }
    }
}
