using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class ItemImageRepository : GenericRepository<ItemImage>, IItemImageRepository
    {
        private readonly JewelleryDBContext _dbContext;
        public ItemImageRepository(JewelleryDBContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<ItemImage> UpdateAsync(string id, ItemImage itemImg)
        {
            var ImageToUpdate = await GetByIdAsync(id);
            if (ImageToUpdate != null)
            {
                Update(itemImg);
                await _dbContext.SaveChangesAsync();
            }
            return itemImg;
        }

        public async Task DeleteAsync(string id)
        {
            var itemImg = await GetByIdAsync(id);
            if (itemImg != null)
            {
                Remove(itemImg);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
