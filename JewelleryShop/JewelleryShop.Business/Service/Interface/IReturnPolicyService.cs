using JewelleryShop.DataAccess.Models.ViewModel.ReturnPolicyViewModel;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service.Interface
{
    public interface IReturnPolicyService
    {
        public Task<List<ReturnPolicy>> GetAllReturnPolicy();
        public Task<ReturnPolicy> GetReturnPolicyByID(string id);
        public Task<ReturnPolicyUpdateDTO> UpdateReturnPolicy(string returnPolicyID, ReturnPolicyUpdateDTO returnPolicy);
        public Task DeleteReturnPolicy(string returnPolicyID);

    }
}
