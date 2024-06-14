﻿using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
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

        public void Approve(Discount dis)
        {
            dis.Status = "Approved";
            _unitOfWork.DiscountRepository.Update(dis);
            _unitOfWork.SaveChangeAsync();
        }

        public Task<List<Discount>> GetAllAsync()
        {
            return _unitOfWork.DiscountRepository.GetAllAsync();
        }

        public Task<Discount> GetByIdAsync(int id)
        {
            return _unitOfWork.DiscountRepository.GetByIdAsync(id);
        }

        public void RemoveAsync(Discount dis)
        {
            _unitOfWork.DiscountRepository.Remove(dis);
        }

        public void Request(Discount dis)
        {
            dis.Status = "Pending";
            _unitOfWork.DiscountRepository.Update(dis);
            _unitOfWork.SaveChangeAsync();
        }

        public void Update(Discount dis)
        {
            _unitOfWork.DiscountRepository.Update(dis);
            _unitOfWork.SaveChangeAsync();
        }
    }
}
