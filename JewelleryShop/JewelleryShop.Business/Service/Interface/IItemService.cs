using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service.Interface
{
    public interface IItemService
    {
        public Task<List<Item>> GetAllAsync();
        public Task<Item> GetByIdAsync(string id);
        public List<Item> SearchByName(string itemName);
        public Task AddAsync(ItemDto item);
        Task UpdateAsync(string id, ItemDto item);
        public Task SoftDelete(string id);
        public Task RemoveAsync(string id);
        public Task<Pagination<Item>> GetPaginatedItemsAsync(int pageIndex, int pageSize);
    }
} 
