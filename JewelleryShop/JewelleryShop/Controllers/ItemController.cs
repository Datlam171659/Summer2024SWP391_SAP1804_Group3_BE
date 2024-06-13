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

        public ItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> ListItems()
        {
            var list = await _unitOfWork.ItemRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchItem(string id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem(Item item) {
            _unitOfWork.ItemRepository.AddAsync(item);
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

       
    }
}
