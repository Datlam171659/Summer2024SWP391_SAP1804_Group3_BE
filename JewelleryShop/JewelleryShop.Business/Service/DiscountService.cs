using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DiscountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Discount dis)
        {
            var newDis = _mapper.Map<Discount>(dis);
            await _unitOfWork.DiscountRepository.AddAsync(newDis);
            await _unitOfWork.SaveChangeAsync();
        }

        public async void Approve(Discount dis)
        {
            dis.Status = "Duyệt";
            _unitOfWork.DiscountRepository.Update(dis);
            _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<Discount>> GetAllAsync()
        {
            return await _unitOfWork.DiscountRepository.GetAllAsync();
        }

        public async Task<Discount> GetByIdAsync(int id)
        {
            return await _unitOfWork.DiscountRepository.GetByIdAsync(id);
        }

        public async void RemoveAsync(Discount dis)
        {
            _unitOfWork.DiscountRepository.Remove(dis);
            _unitOfWork.SaveChangeAsync();
        }

        public async void Request(Discount dis)
        {
            dis.Status = "Chờ duyệt";
            _unitOfWork.DiscountRepository.Update(dis);
            _unitOfWork.SaveChangeAsync();
        }

        public async void Update(Discount dis)
        {
            _unitOfWork.DiscountRepository.Update(dis);
            _unitOfWork.SaveChangeAsync();
        }
    }
}
