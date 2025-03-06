using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelleryShop.DataAccess.Repository.Interface;

namespace JewelleryShop.Business.Service
{
    public class PriceMultiplierService :  IPriceMultiplierService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PriceMultiplierService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PriceMultiplier> GetPriceMultiplierById(int id)
        {
            var e = await _unitOfWork.PriceMultiplierRepository.GetByIdAsync(id);

            if (e == null)
            {
                return null;
            }

            return e;
        }

        public async Task<PriceMultiplier> UpdatePriceMultiplier(PriceMultiplier objIn)
        {
            _unitOfWork.PriceMultiplierRepository.Update(objIn);
            await _unitOfWork.SaveChangeAsync();
            return objIn;
        }
    }
}