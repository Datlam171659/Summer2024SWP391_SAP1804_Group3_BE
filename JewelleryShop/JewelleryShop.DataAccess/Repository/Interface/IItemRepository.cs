using JewelleryShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository.Interface
{
    public interface IItemRepository
    {
        public Task<List<Item>> GetAllAsync();
        public Task<Item> GetByIdAsync(string id);
        public void Update(Item item);
        public Task AddAsync(Item item);
        public void SoftDelete(Item item);
        public void Remove(Item item);
    }
}
