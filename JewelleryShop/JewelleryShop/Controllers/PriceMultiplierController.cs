using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.CollectionViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.GemstoneViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PriceMultiplierController : ControllerBase
    {
        private readonly IPriceMultiplierService _priceMultiplierService;
        public PriceMultiplierController(IPriceMultiplierService priceMultiplierService)
        {
            _priceMultiplierService = priceMultiplierService;
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriceMultiplierById(int id)
        {
            var gemstones = await _priceMultiplierService.GetPriceMultiplierById(id);
            if (gemstones == null)
            {
                var response = APIResponse<CollectionCommonDTO>
                    .ErrorResponse(new List<string> { "No multiplier found with the provided ID." });
                return NotFound(response);
            }
            return Ok(gemstones);
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPut]
        public async Task<IActionResult> UpdatePriceMultiplierAsync(PriceMultiplier obj)
        {
            try
            {
                var updated = await _priceMultiplierService.UpdatePriceMultiplier(obj);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                var response = APIResponse<CollectionInputDTO>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

    }
}
