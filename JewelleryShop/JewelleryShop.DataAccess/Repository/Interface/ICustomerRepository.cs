using JewelleryShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIDAsync(string id);
        Task<Customer> GetByPhoneNumberAsync(string phoneNumber);
        Task<Customer> GetByEmailAsync(string email);
        void Add(Customer customerEntity);
        void Update(Customer existingCustomer);


    }
}
