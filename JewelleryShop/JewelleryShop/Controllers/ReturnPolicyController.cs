using AutoMapper;
using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.RewardsProgramViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnPolicyController : ControllerBase
    {
        private readonly IReturnPolicyService _returnPolicyService;
        public ReturnPolicyController(IReturnPolicyService returnPolicyService)
        {
            _returnPolicyService = returnPolicyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRewardsProgram()
        {
            var result = await _returnPolicyService.GetAllRewardsProgram();
            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<RewardsProgramCommonDTO>> GetRewardProgram(string customerId)
        {
            //var rewardProgram = await _rewardsProgramService.GetRewardsProgramByCustomerIdAsync(customerId);
            //if (rewardProgram == null) return NotFound();

            //return Ok(rewardProgram);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateRewardProgram(string customerId, int points)
        {
            //await _rewardsProgramService.UpdateRewardsProgramAsync(customerId, points);
            //return Ok(APIResponse<string>.SuccessResponse(string.Empty, "Data updated successfully."));
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteRewardProgram(string customerId)
        {
            //await _rewardsProgramService.DeleteRewardsProgramAsync(customerId);
            //return Ok(APIResponse<string>.SuccessResponse(string.Empty, "Data deleted successfully."));
        }

        [HttpPost]
        public async Task<ActionResult<RewardsProgramCommonDTO>> CreateRewardProgram(RewardsProgramInputDTO rewardsProgramInputDTO)
        {
            //var createdRewardProgram = await _rewardsProgramService.AddRewardsProgram(rewardsProgramInputDTO);

            //return CreatedAtAction(nameof(GetRewardProgram), new { customerId = createdRewardProgram.CustomerId }, createdRewardProgram);
        }
    }
}



