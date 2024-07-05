﻿using JewelleryShop.Business.Service;
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
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService; 
        }


        [HttpGet]
        public async Task<IActionResult> ListItems()
        {
            try
            {
                var list = await _itemService.GetAllAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


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


        [HttpGet("search")]
        public IActionResult SearchItemByName(string itemName)
        {
            try
            {
                var items = _itemService.SearchByName(itemName);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemDto item) {
            await _itemService.AddAsync(item);
            return Ok(APIResponse<string>.SuccessResponse(data:null, "Create successfully."));
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItem(string id, ItemDto item)
        {
            await _itemService.UpdateItemAsync(id, item);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update successfully."));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                await _itemService.RemoveAsync(id);
                return Ok(APIResponse<string>.SuccessResponse(data: null, "Delete Successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("softdelete/{id}")]
        public async Task<IActionResult> SoftDeleteItem(string id)
        {
            await _itemService.SoftDelete(id);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Disable Successfully."));
        }


        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedItems([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var paginatedItems = await _itemService.GetPaginatedItemsAsync(pageIndex, pageSize);
            return Ok(paginatedItems);
        }


        [HttpPut("updateQuantity/{id}")]
        public async Task<IActionResult> UpdateQuantity(string id, int quantity)
        {
            await _itemService.UpdateQuantityAsync(id, quantity);
            return Ok(APIResponse<string>.SuccessResponse(data: null, "Update successfully."));
        }


    }
}
