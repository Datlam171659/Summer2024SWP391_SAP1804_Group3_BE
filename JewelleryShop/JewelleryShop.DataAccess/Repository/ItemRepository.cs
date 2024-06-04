using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private readonly JewelleryDBContext _context;
        public ItemRepository(JewelleryDBContext context) : base(context)
        {
            _context = context;
        }

        public Task AddAsync(Item entity)
        {
            try
            {
                var item = _context.AddAsync(entity);
                return Task.FromResult(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot create new item: " + ex.Message);
            }
        }


        public async Task<Item?> GetByIdAsync(string id)
        {
            try
            {
                var item = await _context.Items.FindAsync(id);
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot find item by id" + ex.Message);
            }
        }

        public async void Remove(Item item)
        {
            try
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot delete item:" + ex.Message);
            }
        }

        public async void SoftDelete(Item item)
        {
            try
            {
                string iStatus = "Out stock";
                iStatus = item.Status;
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot update status item:" + ex.Message);
            }
        }

        public void Update(Item item)
        {
            try
            {
                _context.Items.Update(item);
  
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot update item:" + ex.Message);
            }
        }
    }
}
