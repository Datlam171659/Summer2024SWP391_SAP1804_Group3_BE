using System;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JewelleryShop.DataAccess.Models;

using JewelleryShop.DataAccess.Models.dto;

using JewelleryShop.DataAccess.Models.ViewModel.CustomerViewModel;

using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.StaffViewModel;
using JewelleryShop.DataAccess.Models.ViewModel.WarrantyViewModel;

namespace JewelleryShop.DataAccess.Utils
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Invoice, InvoiceCommonDTO>().ReverseMap();
            CreateMap<Warranty, WarrantyCommonDTO>().ReverseMap();

            CreateMap<Invoice, InvoiceInputDTO>().ReverseMap();
            CreateMap<Warranty, WarrantyInputDTO>().ReverseMap();

            CreateMap<Customer, CustomerCommonDTO>().ReverseMap();
            CreateMap<Customer, CustomerInputDTO>().ReverseMap();

            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<staff, StaffCommonDTO>().ReverseMap();
            CreateMap<staff, StaffRegisterDTO>().ReverseMap();

        }
    }
}
