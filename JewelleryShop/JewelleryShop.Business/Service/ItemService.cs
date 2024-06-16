using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            item.Status = "Out stock";
            _unitOfWork.ItemRepository.Update(item);
            _unitOfWork.SaveChangeAsync();
        }

        public async void Update(Item item)
        {
            _unitOfWork.ItemRepository.Update(item);
            _unitOfWork.SaveChangeAsync();

        }
        public async void Pagination()
        {
            _unitOfWork.ItemRepository.ToPagination();
        }
        public async Task<List<Item>> GetByName(string itemName)
        {
            return await _unitOfWork.ItemRepository.GetByNameAsync(itemName);
        }

    }
}
