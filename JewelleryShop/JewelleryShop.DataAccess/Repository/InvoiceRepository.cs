using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly JewelleryDBContext _dbContext;
        public InvoiceRepository(JewelleryDBContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<Invoice> GetByInvoiceIdAsync(string id)
        {
            return await _dbContext.Invoices.FindAsync(id);
        }
        public void Add(Invoice invoiceEntity)
        {
            _dbContext.Invoices.Add(invoiceEntity);
        }
        

    }
}

