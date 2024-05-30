using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.CustomerViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;

namespace JewelleryShop.DataAccess.Utils
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Invoice, InvoiceCommonDTO>().ReverseMap();
            CreateMap<Warranty, WarrantyCommonDTO>().ReverseMap();
            CreateMap<Customer, CustomerCommonDTO>().ReverseMap();
        }
    }
}
