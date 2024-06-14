﻿using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
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
        public void Update(Item item);
        public Task AddAsync(ItemDto item);
        public void SoftDelete(Item item);
        public void RemoveAsync(Item item);
    }
} 
