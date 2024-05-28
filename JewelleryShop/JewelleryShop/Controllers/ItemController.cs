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
        private readonly JewelleryDBContext _context;

        public ItemController(JewelleryDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            var itemId = await _context.Items.FindAsync(id);

            if (itemId == null)
            {
                return NotFound();
            }

            return itemId;
        }


        [HttpPost]
        public IActionResult CreateItem(ItemDto request) {
            var item = new Item()
            {
                ItemId = request.ItemId,
                ItemImagesId = request.ItemImagesId,
                ItemName = request.ItemName,
                BrandId = request.BrandId,
                AccessoryType = request.AccessoryType,
                CreatedDate = request.CreatedDate,
                Description = request.Description,
                Price = request.Price,
                Size = request.Size,
                Sku = request.Sku,
                UpdatedDate = request.UpdatedDate,
                Status = request.Status,
                Weight = request.Weight,
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id, ItemDto request)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.ItemImagesId = request.ItemImagesId;
            item.ItemName = request.ItemName;
            item.BrandId = request.BrandId;
            item.AccessoryType = request.AccessoryType;
            item.CreatedDate = request.CreatedDate;
            item.Description = request.Description;
            item.Price = request.Price;
            item.Size = request.Size;
            item.Sku = request.Sku;
            item.UpdatedDate = request.UpdatedDate;
            item.Status = request.Status;
            item.Weight = request.Weight;

            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            return NoContent();
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
    }
}
