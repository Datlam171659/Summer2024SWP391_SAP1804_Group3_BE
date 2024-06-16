﻿using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JewelleryDBContext _dbContext;
        private readonly IStaffRepository _staffRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IWarrantyRepository _warrantyRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ICustomerPromotionRepository _customerPromotionRepository;

        private readonly IStaffStationRepository _staffStationRepository; 
        private readonly IRewardsProgramRepository _rewardsProgramRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IGemstoneRepository _gemstoneRepository;
        public UnitOfWork(
            JewelleryDBContext dbContext,
            IStaffRepository staffRepository,
            ICustomerRepository customerRepository,
            IInvoiceRepository invoiceRepository,
            IWarrantyRepository warrantyRepository,
            IItemRepository itemRepository,
            IDiscountRepository discountRepository,
            ICustomerPromotionRepository customerPromotionRepository,
            IStaffStationRepository staffShiftRepository,
            IRewardsProgramRepository rewardsProgramRepository,
            ICollectionRepository collectionRepository,
            IGemstoneRepository gemstoneRepository

        )
        {
            _dbContext = dbContext;
            _staffRepository = staffRepository;
            _customerRepository = customerRepository;
            _invoiceRepository = invoiceRepository;
            _warrantyRepository = warrantyRepository;
            _itemRepository = itemRepository;
            _discountRepository = discountRepository;
            _customerPromotionRepository = customerPromotionRepository;
            _staffStationRepository = staffShiftRepository;
            _rewardsProgramRepository = rewardsProgramRepository;
            _collectionRepository = collectionRepository;
            _gemstoneRepository = gemstoneRepository;   
        }

        public IStaffRepository StaffRepository => _staffRepository;
        public ICustomerRepository CustomerRepository => _customerRepository;
        public IInvoiceRepository InvoiceRepository => _invoiceRepository;
        public IWarrantyRepository WarrantyRepository => _warrantyRepository;
        public IItemRepository ItemRepository => _itemRepository;
        public IDiscountRepository DiscountRepository => _discountRepository;
        public ICustomerPromotionRepository CustomerPromotionRepository => _customerPromotionRepository;    
        public IStaffStationRepository StaffStationRepository => _staffStationRepository; 
        public IRewardsProgramRepository RewardsProgramRepository => _rewardsProgramRepository;
        public ICollectionRepository CollectionRepository => _collectionRepository;
        public IGemstoneRepository GemstoneRepository => _gemstoneRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
