using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.CollectionViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    [Authorize(Roles = "Admin, Manager, Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalAPIController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IGoldPriceService _goldPriceService;

        public ExternalAPIController(IExchangeRateService exchangeRateService, IGoldPriceService goldPriceService)
        {
            _exchangeRateService = exchangeRateService;
            _goldPriceService = goldPriceService;
        }

        [HttpGet("ExchangeRate")]
        public async Task<IActionResult> GetLatestRates()
        {
            try
            {
                var res = await _exchangeRateService.GetLatestRatesAsync();
                return Ok(
                    APIResponse<ExchangeRateResponse>.SuccessResponse(
                        data: res,
                        message: "Successfully fetched latest rates."
                    )
                );
            }
            catch (Exception ex)
            {
                var response = APIResponse<CollectionInputDTO>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

        [HttpGet("ExchangeRate/vnd")]
        public async Task<IActionResult> GetVNDRates()
        {
            try
            {
                var res = await _exchangeRateService.GetRateForVndAsync();
                return Ok(
                    APIResponse<string>.SuccessResponse(
                        data: res.ToString(),
                        message: "Successfully fetched latest VND rates."
                    )
                );
            }
            catch (Exception ex)
            {
                var response = APIResponse<CollectionInputDTO>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

        [HttpGet("GoldPrice")]
        public async Task<IActionResult> GetGoldPrices()
        {
            try
            {
                var res = await _goldPriceService.GetGoldPriceAsync();
                return Ok(
                    APIResponse<string>.SuccessResponse(
                        data: res.ToString(),
                        message: "Successfully fetched latest gold price."
                    )
                );
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
