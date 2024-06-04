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
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(ItemDto item)
        {
            var itemToAdd = _mapper.Map<Item>(item);
            await _unitOfWork.ItemRepository.AddAsync(itemToAdd);
            await _unitOfWork.SaveChangeAsync();
        }

        public Task<List<Item>> GetAllAsync()
        {
            return _itemRepository.GetAllAsync();
        }

        public Task<Item> GetByIdAsync(string id)
        {
            return _itemRepository.GetByIdAsync(id);
        }

        public void RemoveAsync(ItemDto item)
        {
            var itemtoDelete = _mapper.Map<Item>(item);
            _unitOfWork.ItemRepository.Remove(itemtoDelete);
            _unitOfWork.SaveChangeAsync();
        }

        public void SoftDelete(ItemDto item)
        {
            var itemtoSoftDelete = _mapper.Map<Item>(item);
            _unitOfWork.ItemRepository.SoftDelete(itemtoSoftDelete);
            _unitOfWork.SaveChangeAsync();
        }

        public void Update(ItemDto item)
        {
            var itemtoUpdate = _mapper.Map<Item>(item);
            _unitOfWork.ItemRepository.Update(itemtoUpdate);
            _unitOfWork.SaveChangeAsync();

        }
    }
}
