using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class WarrantyRepository : GenericRepository<Warranty>, IWarrantyRepository
    {
        private readonly JewelleryDBContext _dbContext;
        public WarrantyRepository(JewelleryDBContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
        public void Add(Warranty warrantyEntity)
        {
            _dbContext.Warranties.Add(warrantyEntity);
        }
        public async Task<Warranty> GetByWarrantyIdAsync(string id)
        {
            return await _dbContext.Warranties.FindAsync(id);
        }

    }
}
