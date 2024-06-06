using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service.Interface
{
    public interface IDiscountService
    {
        public Task<List<Discount>> GetAllAsync();
        public Task<Discount> GetByIdAsync(int id);
        public void Update(Discount dis);
        public Task AddAsync(Discount dis);
        public void RemoveAsync(Discount dis);
        public void Approve(Discount dis);
        public void Request(Discount dis);
    }
}
