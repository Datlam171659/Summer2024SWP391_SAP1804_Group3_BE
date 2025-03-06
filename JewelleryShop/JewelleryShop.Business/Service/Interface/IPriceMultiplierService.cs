using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service.Interface
{
    public interface IPriceMultiplierService
    {
        Task<PriceMultiplier> GetPriceMultiplierById(int id);
        Task<PriceMultiplier> UpdatePriceMultiplier(PriceMultiplier objIn);
    }
}
