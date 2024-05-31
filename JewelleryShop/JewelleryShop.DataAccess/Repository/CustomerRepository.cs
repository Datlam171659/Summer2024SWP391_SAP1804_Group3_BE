using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly JewelleryDBContext _dbContext;

        public CustomerRepository(JewelleryDBContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
        public void Add(Customer customerEntity)
        {
            _dbContext.Customers.Add(customerEntity);
        }

        public void Update(Customer existingCustomer)
        {
            _dbContext.Entry(existingCustomer).State = EntityState.Modified;
        }

        public async Task<Customer> GetByIDAsync(string id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }
    }
}
