using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelleryShop.DataAccess.Models;

namespace JewelleryShop.DataAccess.Repository.Interface
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<List<Discount>> GetAllAsync();
        Task<Discount?> GetByIdAsync(int id);
        Task AddAsync(Discount entity);
        void Update(Discount entity);
        void Remove(Discount entity);
        void Approve(Discount entity);
        void Request(Discount entity);


    }
}
