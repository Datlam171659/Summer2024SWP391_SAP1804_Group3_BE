﻿using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelleryShop.DataAccess.Models.ViewModel.ReturnPolicyViewModel;
using System.Net;

namespace JewelleryShop.Business.Service
{
    public class ReturnPolicyService : IReturnPolicyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReturnPolicyService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReturnPolicy>> GetAllReturnPolicy()
        {
            return await _unitOfWork.ReturnPolicyRepository.GetAllAsync();
        }
        public async Task<ReturnPolicy> GetReturnPolicyByID(string id)
        {
            return await _unitOfWork.ReturnPolicyRepository.GetByIdAsync(id);
        }
        public async Task<ReturnPolicyUpdateDTO> UpdateReturnPolicy(string returnPolicyID, ReturnPolicyUpdateDTO returnPolicy)
        {
            var Dest_ReturnPolicy = await _unitOfWork.ReturnPolicyRepository.GetByIdAsync(returnPolicyID);
            _mapper.Map(returnPolicy, Dest_ReturnPolicy);
            _unitOfWork.ReturnPolicyRepository.Update(Dest_ReturnPolicy);
            await _unitOfWork.SaveChangeAsync();

            return returnPolicy;
        }
        public async Task DeleteReturnPolicy(string returnPolicyID)
        {
            var ReturnPolicy = await _unitOfWork.ReturnPolicyRepository.GetByIdAsync(returnPolicyID);
            if (ReturnPolicy != null)
            {
                _unitOfWork.ReturnPolicyRepository.Remove(ReturnPolicy);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new ArgumentException("No Return Policy found with the provided ID.");
            }
        }
    }
}