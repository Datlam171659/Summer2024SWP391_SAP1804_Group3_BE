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

        public void RemoveAsync(Item item)
        {
            _unitOfWork.ItemRepository.Remove(item);
            _unitOfWork.SaveChangeAsync();
        }

        public void SoftDelete(Item item)
        {
            item.Status = "Out stock";
            _unitOfWork.ItemRepository.Update(item);
            _unitOfWork.SaveChangeAsync();
        }

        public void Update(Item item)
        {
            _unitOfWork.ItemRepository.Update(item);
            _unitOfWork.SaveChangeAsync();

        }
    }
}
