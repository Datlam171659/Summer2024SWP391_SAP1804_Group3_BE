using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.PromotionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service
{
    public class CustomerPromotionService : ICustomerPromotionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerPromotionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(CustomerPromotionDto obj)
        {
            var itemToAdd = _mapper.Map<CustomerPromotion>(obj);
            await _unitOfWork.CustomerPromotionRepository.AddAsync(itemToAdd);
            await _unitOfWork.SaveChangeAsync();
        }

        public async void Approve(CustomerPromotion obj)
        {
            obj.Status = "Duyệt";
            _unitOfWork.CustomerPromotionRepository.Update(obj);
            _unitOfWork.SaveChangeAsync();
        }

        public async void Delete(CustomerPromotion obj)
        {
            _unitOfWork.CustomerPromotionRepository.Remove(obj);
            _unitOfWork.SaveChangeAsync();
        }

        public async void Update(CustomerPromotion obj)
        {
            _unitOfWork.CustomerPromotionRepository.Update(obj);
            _unitOfWork.SaveChangeAsync();
        }
    }
}
