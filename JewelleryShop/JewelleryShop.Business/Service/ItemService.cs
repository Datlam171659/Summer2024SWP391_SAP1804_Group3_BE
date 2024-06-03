using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Repository.Interface;
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

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public Task AddAsync(ItemDto item)
        {
            var itemToAdd = _mapper.Map<Item>(item);
            return _itemRepository.AddAsync(itemToAdd);
        }

        public Task<List<Item>> GetAllAsync()
        {
            return _itemRepository.GetAllAsync();
        }

        public Task<Item> GetByIdAsync(string id)
        {
            return _itemRepository.GetByIdAsync(id);
        }

        public void Remove(ItemDto item)
        {
            return _itemRepository.Remove(item);
        }

        public void SoftDelete(ItemDto item)
        {
            throw new NotImplementedException();
        }

        public void Update(ItemDto item)
        {
            throw new NotImplementedException();
        }
    }
}
