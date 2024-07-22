using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.ItemImageViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.ItemViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace JewelleryShop.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // dependency injection
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService; 
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet]
        public async Task<IActionResult> ListItems()
        {
            var list = await _itemService.GetAllAsync();
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> SearchItemByID(string id)
        {
            var item = await _itemService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("search")]
        public IActionResult SearchItemByName(string itemName)
        {
            
            try
            {
                var item = _itemService.SearchByName(itemName);
                return Ok(item);
            }
            catch
            {
                return BadRequest("Item with this name does not exist ...");
            }
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemCreateDTO item) {
            await _itemService.AddAsync(item);
            return Ok(APIResponse<string>.SuccessResponse(data:null, "Create successfully."));
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItem(string id, ItemDTO item)
        {
            await _itemService.UpdateItemAsync(id, item);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update successfully."));
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                await _itemService.SoftDelete(id);
                return Ok(APIResponse<string>.SuccessResponse(data: null, "Delete Successfully."));
            }
            catch (Exception ex)
            {
                var response = APIResponse<string>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }


        //[HttpPut("softdelete/{id}")]
        //public async Task<IActionResult> SoftDeleteItem(string id)
        //{
        //    await _itemService.SoftDelete(id);
        //    return Ok(APIResponse<string>.SuccessResponse(data: null, "Disable Successfully."));
        //}

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedItems([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var paginatedItems = await _itemService.GetPaginatedItemsAsync(pageIndex, pageSize);
            return Ok(paginatedItems);
        }

        [Authorize("Admin, Manager, Staff")]
        [HttpPut("updateQuantity/{id}")]
        public async Task<IActionResult> UpdateQuantity(string id, int quantity)
        {
            await _itemService.UpdateQuantityAsync(id, quantity);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update successfully."));
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("GetAllBuyBack")]
        public async Task<IActionResult> ListAllBuyBack()
        {
            var list = await _itemService.GetAllBuyBackAsync();
            return Ok(
                APIResponse<List<ItemDTO>>.SuccessResponse(data: list, "Fetched Buyback items successfully.")
            );
        }
    }
}
