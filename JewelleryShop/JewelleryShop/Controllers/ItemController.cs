using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // dependency injection
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService) {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> ListItems()
        {
            var list = await _itemService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchItem(string id)
        {
            var item = await _itemService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemDto request) {
            await _itemService.AddAsync(request);
            return Ok(APIResponse<string>
                    .SuccessResponse(data:null, "Create successully.")
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id, Item request)
        {
            _itemService.Update(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id, Item request)
        {
            _itemService.RemoveAsync(request);
            return NoContent();
        }

        [HttpPut("softdelete/{id}")]
        public async Task<IActionResult> SoftDeleteItem(string id)
        {
            var item = await _itemService.GetByIdAsync(id);
            _itemService.SoftDelete(item);
            return NoContent();
        }
    }
}
