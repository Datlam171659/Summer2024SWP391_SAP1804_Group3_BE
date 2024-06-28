using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.CollectionViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.Commons;
using JewelleryShop.DataAccess.Models.ViewModel.ItemImageViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemImageController: ControllerBase 
    {
        private readonly IItemImageService _itemImageService;


        public ItemImageController(IItemImageService itemImageService)
        {
            _itemImageService = itemImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemImages()
        {
            var imgs = await _itemImageService.GetAllItemImage();
            return Ok(imgs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemImagesById(string id)
        {
            try
            {
                var img = await _itemImageService.GetItemImageById(id);
                if (img == null)
                {
                    var response = APIResponse<CollectionCommonDTO>
                        .ErrorResponse(new List<string> { "No items found with the provided ID." });
                    return NotFound(response);
                }
                return Ok(img);
            }
            catch (Exception ex)
            {
                var response = APIResponse<ItemImageInputDTO>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCollection(ItemImageInputDTO img_items)
        {
            try
            {
                var newImgItems = await _itemImageService.AddItemImage(img_items);
                return CreatedAtAction(nameof(GetItemImagesById), new { id = newImgItems.Id }, newImgItems);
            }
            catch (Exception ex)
            {
                var response = APIResponse<ItemImageInputDTO>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

        [HttpPut("ItemImages/{id}")]
        public async Task<IActionResult> UpdateItemImagesAsync(string id, ItemImageInputDTO imgDTO)
        {
            try
            {
                var updatedImages = await _itemImageService.UpdateItemImageAsync(id, imgDTO);
                return Ok(updatedImages);
            }
            catch (Exception ex)
            {
                var response = APIResponse<ItemImageInputDTO>
                    .ErrorResponse(new List<string> { ex.Message });
                return BadRequest(response);
            }
        }

        [HttpDelete("ItemImages/{id}")]
        public async Task<IActionResult> DeleteItemImagesAsync(string id)
        {
            try
            {
                await _itemImageService.DeleteItemImageAsync(id);
                var response = APIResponse<string>.SuccessResponse(id, "Items deleted successfully");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                var response = APIResponse<string>
                    .ErrorResponse(new List<string> { ex.Message });
                return NotFound(response);
            }
        }
    }
}
