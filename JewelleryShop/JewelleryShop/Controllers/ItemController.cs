using JewelleryShop.Business.Service;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemService _itemService;

        public ItemController(IUnitOfWork unitOfWork, IItemService itemService)
        {
            _unitOfWork = unitOfWork;
            _itemService = itemService; 
        }

        [HttpGet]
        public async Task<IActionResult> ListItems()
        {
            var list = await _unitOfWork.ItemRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchItemByID(string id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("search")]
        public IActionResult SearchItemByName(string itemName)
        {
            try
            {
                var item = _unitOfWork.ItemRepository.GetByName(itemName);
                return Ok(item);
            }
            catch
            {
                return BadRequest("Item with this name does not exist ...");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemDto item) {
            _itemService.AddAsync(item);
            return Ok(APIResponse<string>.SuccessResponse(data:null, "Create successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            _unitOfWork.ItemRepository.Update(item);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update Successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            _unitOfWork.ItemRepository.Remove(item);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Delete Successfully."));
        }
    

        [HttpPut("softdelete/{id}")]
        public async Task<IActionResult> SoftDeleteItem(string id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            _unitOfWork.ItemRepository.SoftDelete(item);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Disable Successfully."));
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedItems([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var paginatedItems = await _itemService.GetPaginatedItemsAsync(pageIndex, pageSize);
            return Ok(paginatedItems);
        }
    }
}
