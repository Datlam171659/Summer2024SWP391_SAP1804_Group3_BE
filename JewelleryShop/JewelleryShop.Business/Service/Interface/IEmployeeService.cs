﻿using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.EmployeeViewModel;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service.Interface
{
    public interface IEmployeeService : IGenericRepository<Employee>
    {
        public Task<string> LoginAsync(EmployeeLoginDTO employee);
    }
}
