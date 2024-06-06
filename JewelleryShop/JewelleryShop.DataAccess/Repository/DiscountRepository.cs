using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelleryShop.DataAccess.Models;


namespace JewelleryShop.DataAccess.Repository
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        private readonly JewelleryDBContext _context;

        public DiscountRepository(JewelleryDBContext context) : base(context)
        {
            _context = context;
        }

        public Task AddAsync(Discount entity)
        {
            try
            {
                var discount = _context.AddAsync(entity);
                return Task.FromResult(discount);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot create new discount: " + ex.Message);
            }
        }

        public void Approve(Discount entity)
        {
            try
            {
                _context.Discounts.Update(entity);
            }
            catch(Exception ex)
            {
                throw new Exception("Cannot approve discount: " + ex.Message);
            }
        }

        public async Task<Discount?> GetByIdAsync(int id)
        {
            try
            {
                var discount = await _context.Discounts.FindAsync(id);
                return discount;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot find item by id" + ex.Message);
            }
        }

        public Task<Discount?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Discount entity)
        {
            try
            {
                _context.Discounts.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot approve discount: " + ex.Message);
            }
        }

        public void Request(Discount entity)
        {
            try
            {
                _context.Discounts.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot approve discount: " + ex.Message);
            }
        }

        public void Update(Discount entity)
        {
            try
            {
                _context.Discounts.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot approve discount: " + ex.Message);
            }
        }
    }
}
