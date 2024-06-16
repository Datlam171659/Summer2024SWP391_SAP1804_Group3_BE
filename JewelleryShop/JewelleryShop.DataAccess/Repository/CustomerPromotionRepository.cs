using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{

    public class CustomerPromotionRepository : GenericRepository<CustomerPromotion>, ICustomerPromotionRepository
    {
        private readonly JewelleryDBContext _dbContext;
        public CustomerPromotionRepository(JewelleryDBContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
