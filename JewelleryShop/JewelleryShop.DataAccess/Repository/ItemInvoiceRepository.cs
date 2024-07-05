using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class ItemInvoiceRepository : IItemInvoiceRepository
    {
        private readonly JewelleryDBContext _context;

        public ItemInvoiceRepository(JewelleryDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemInvoice>> GetByItemIdAsync(string itemId)
        {
            return await _context.ItemInvoices.Where(ii => ii.ItemId == itemId).ToListAsync();
        }
    }
}
