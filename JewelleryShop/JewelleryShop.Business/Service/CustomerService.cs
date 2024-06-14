using AnyAscii;
using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.CustomerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyAscii;

namespace JewelleryShop.Business.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<CustomerCommonDTO>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerCommonDTO>>(await _unitOfWork.CustomerRepository.GetAllAsync());
        }
        public async Task<CustomerCommonDTO> GetByIDAsync(string id)
        {
            var entity = await _unitOfWork.CustomerRepository.GetByIDAsync(id);
            return entity == null ? null : _mapper.Map<CustomerCommonDTO>(entity);
        }

        public async Task<CustomerCommonDTO> GetByEmailAsync(string email)
        {
            var entity = await _unitOfWork.CustomerRepository.GetByEmailAsync(email);
            return entity == null ? null : _mapper.Map<CustomerCommonDTO>(entity);
        }

        public async Task<CustomerCommonDTO> GetByPhoneNumberAsync(string phoneNumber)
        {
            var entity = await _unitOfWork.CustomerRepository.GetByPhoneNumberAsync(phoneNumber);
            return entity == null ? null : _mapper.Map<CustomerCommonDTO>(entity);
        }

        private string RemoveDiacritics(string text)
        {

            return text.Transliterate();
        }
        private string GenerateCustomerId(string name, DateTime creationDate)
        {
            name = RemoveDiacritics(name);
            var initials = string.Join("", name.Split(' ').Take(3).Select(x => x[0]).ToArray()).ToUpper();
            var formattedDate = creationDate.ToString("ddMMyyHHmmss");
            return $"{initials}{formattedDate}";
        }
        public async Task<CustomerCommonDTO> CreateCustomerAsync(CustomerInputDTO customerData)
        {

            var customerEntity = _mapper.Map<Customer>(customerData);
            customerEntity.Id = GenerateCustomerId(customerData.CustomerName, DateTime.Now);

            await _unitOfWork.CustomerRepository.AddAsync(customerEntity);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CustomerCommonDTO>(customerEntity);
        }

        public async Task<CustomerInputDTO> UpdateCustomerAsync(string id, CustomerInputDTO newCustomerData)
        {
            var existingCustomer = await _unitOfWork.CustomerRepository.GetByIDAsync(id);

            if (existingCustomer == null)
                return null;

            _mapper.Map(newCustomerData, existingCustomer);

            _unitOfWork.CustomerRepository.Update(existingCustomer);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CustomerInputDTO>(existingCustomer);
        }
    }
}

