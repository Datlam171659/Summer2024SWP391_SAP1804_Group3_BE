using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

        // GET: api/item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> ListItems()
        {
            return await _itemService.GetAllAsync();
        }

        // GET: api/item/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> SearchItem(string id)
        {
            var item = await _itemService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }


        [HttpPost]
        public IActionResult CreateItem(ItemDto request) {
            return Ok(_itemService.AddAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id, ItemDto request)
        {
            return ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("softdelete/{id}")]
        public async Task<IActionResult> SoftDeleteItem(string id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.Status = "out stock";
            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
