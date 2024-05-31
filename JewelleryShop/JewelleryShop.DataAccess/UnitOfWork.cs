using JewelleryShop.DataAccess.Models;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;

        public UnitOfWork(
            JewelleryDBContext dbContext,
            IEmployeeRepository employeeRepository,
            ICustomerRepository customerRepository
        )
        {
            _dbContext = dbContext;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository;
        public ICustomerRepository CustomerRepository => _customerRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
