using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Models.ViewModel.PromotionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service.Interface
{
    public interface ICustomerPromotionService
    {
        public Task<List<CustomerPromotion>> GetAll();
        public Task AddAsync(CustomerPromotionDto obj);
        public void Update(CustomerPromotion obj);
        public void Delete(CustomerPromotion obj);
        public void Approve(CustomerPromotion obj);

    }
}
