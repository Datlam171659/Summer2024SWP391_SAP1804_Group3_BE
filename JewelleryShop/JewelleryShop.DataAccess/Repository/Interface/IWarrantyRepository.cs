﻿using JewelleryShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository.Interface
{
    public interface IWarrantyRepository
    {
        Task<List<Warranty>> GetAllAsync();
        Task<Warranty> GetByWarrantyIdAsync(string id);
        void Add(Warranty warrantyEntity);
    }
}
