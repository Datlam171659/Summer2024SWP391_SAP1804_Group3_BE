using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Repository.Interface;
using JewelleryShop.DataAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service
{
    public class ItemService : IItemService
    {
        // dependency injection
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(ItemDto item)
        {
            var itemToAdd = _mapper.Map<Item>(item);
            await _unitOfWork.ItemRepository.AddAsync(itemToAdd);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _unitOfWork.ItemRepository.GetAllAsync();
        }

        public async Task<Item> GetByIdAsync(string id)
        {
            return await _unitOfWork.ItemRepository.GetByIdAsync(id);
        }

        public async Task RemoveAsync(string id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.ItemRepository.Remove(item);
                _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Can not delete Item");
            }
        }

        public async Task SoftDelete(string id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                item.Status = "Hết hàng";
                _unitOfWork.ItemRepository.Update(item);
                await _unitOfWork.SaveChangeAsync();
            }
            else 
            {
                throw new Exception("Can not update Item status");
            }
        }

        public async Task UpdateAsync(string id, ItemDto item)
        {
            var itemToUpdate = await GetByIdAsync(id);
     
            if (itemToUpdate != null)
            {
                itemToUpdate = _mapper.Map<ItemDto, Item>(item, itemToUpdate);
                _unitOfWork.ItemRepository.Update(itemToUpdate);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Can not update Item");
            }
        }

        public async Task<Pagination<Item>> GetPaginatedItemsAsync(int pageIndex, int pageSize)
        {
            return await _unitOfWork.ItemRepository.ToPagination(pageIndex, pageSize);
        }

        public List<Item> SearchByName(string itemName)
        {
            return _unitOfWork.ItemRepository.GetByName(itemName);
        }
    }
}
