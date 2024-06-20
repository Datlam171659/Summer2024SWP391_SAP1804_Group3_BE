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
        private readonly JewelleryDBContext _context;

        public ItemService(IMapper mapper, IUnitOfWork unitOfWork, JewelleryDBContext context)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context; 
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

        public void RemoveAsync(Item item)
        {
            _unitOfWork.ItemRepository.Remove(item);
            _unitOfWork.SaveChangeAsync();
        }

        public async void SoftDelete(Item item)
        {
            item.Status = "Hết hàng";
            _unitOfWork.ItemRepository.Update(item);
            _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateAsync(string id, ItemDto item)
        {
            var itemToUpdate = await _unitOfWork.ItemRepository.GetByIdAsync(id);
     
            if (itemToUpdate != null)
            {
                _mapper.Map(item, itemToUpdate);
                _unitOfWork.ItemRepository.Update(itemToUpdate);
                await _unitOfWork.SaveChangeAsync();
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
