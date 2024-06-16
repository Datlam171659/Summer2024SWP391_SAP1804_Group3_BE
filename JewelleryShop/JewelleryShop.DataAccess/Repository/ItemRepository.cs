﻿using JewelleryShop.DataAccess.Models;
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
        public void SoftDelete(Item item)
        {
            try
            {
                _context.Items.Update(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot update status item:" + ex.Message);
            }
        }

        public async Task<List<Item>> GetByNameAsync(string itemName) 
        {
            var items = _context.Items.Where(Item => Item.ItemName.Contains(itemName));
            var result = items.Select(Item => new Item
            {
                ItemId = Item.ItemId,
                ItemName = Item.ItemName,
                SerialNumber = Item.SerialNumber,
                Price = Item.Price,
                AccessoryType = Item.AccessoryType
            });
            return result.ToList();
        }

    }
}
