using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class PriceMultiplierRepository : GenericRepository<PriceMultiplier>, IPriceMultiplierRepository
    {
        private readonly JewelleryDBContext _dbContext;
        public PriceMultiplierRepository(JewelleryDBContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
