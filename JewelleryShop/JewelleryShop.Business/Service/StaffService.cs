using AutoMapper;
using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.StaffViewModel;
using JewelleryShop.DataAccess.Utils;
using Microsoft.Extensions.Configuration;

namespace JewelleryShop.Business.Service
{
    public class StaffService : IStaffService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffService (IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<StaffCommonDTO> AddEmployeeAsync(StaffRegisterDTO employee)
        {
            staff isDuplicate = await _unitOfWork.StaffRepository.GetByIdAsync(employee.StaffId);
            if (isDuplicate != null) throw new Exception("Duplicate StaffID!");

            employee.PasswordHash = StringUtils.HashPassword(employee.PasswordHash);
            var emp = _mapper.Map<staff>(employee);

            await _unitOfWork.StaffRepository.AddAsync(emp);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<StaffCommonDTO>(emp); ;
        }

        public void DisableAccount(staff staff)
        {
            staff.Status = "Inactive";
            _unitOfWork.StaffRepository.Update(staff);
            _unitOfWork.SaveChangeAsync();
        }

        public async Task<string> LoginAsync(StaffLoginDTO employee)
        {
            var user = await _unitOfWork.StaffRepository.CheckLoginCredentials(employee.UsernameOrEmail, employee.Password);
            if (user != null)
            {
                string jwtKey = _configuration.GetValue<string>("Jwt:SecretKey");
                double expirationTime = _configuration.GetValue<double>("Jwt:ExpiryTimeMinutes");

                return user.GenerateJsonWebToken(jwtKey, DateTime.UtcNow, expiration: expirationTime);
            }
            return null;
        }
    }
}
