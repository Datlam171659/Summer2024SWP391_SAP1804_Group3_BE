using AutoMapper;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Repository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly JewelleryDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        public InvoiceRepository(JewelleryDBContext dbcontext, IMapper mapper, IItemRepository itemRepository) : base(dbcontext)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<InvoiceWithItemsDTO> CreateInvoiceWithItemsAsync(Invoice invoice, IEnumerable<InvoiceInputItemDTO> items, string returnPolicyId, string warrantyId)
        {
            var itemIDAdded = new List<string>();
            foreach (var _item in items)
            {
                var itemInvoice = new ItemInvoice
                {
                    InvoiceId = invoice.Id,
                    ItemId = _item.itemID,
                    WarrantyId = warrantyId,
                    ReturnPolicyId = returnPolicyId
                };
                var item = await _itemRepository.GetByIdAsync(_item.itemID);
                if (item != null && item.Quantity > 0)
                {
                    var itemStockDelta = item.Quantity - _item.itemQuantity;
                    if (itemStockDelta < 0) throw new Exception($"Insufficient stock for Item: {item.ItemName}({item.ItemId})");
                    item.Quantity -= _item.itemQuantity;
                    _itemRepository.Update(item);
                }
                else throw new Exception($"Item: {item.ItemName}({item.ItemId}) is out of stock");
                await _dbContext.ItemInvoices.AddAsync(itemInvoice);
                itemIDAdded.Add(_item.itemID);
            }

            await _dbContext.Invoices.AddAsync(invoice);
            InvoiceWithItemsDTO invoiceWithItems = new InvoiceWithItemsDTO
            {
                InvoiceDetails = _mapper.Map<InvoiceCommonDTO>(invoice),
                itemIds = itemIDAdded,
                returnPolicyId = returnPolicyId,
                warrantyId = warrantyId
            };
            return invoiceWithItems;
        }

        public async Task<List<Item>> GetInvoiceItems(string invoiceID)
        {
            var itemsForInvoice = _dbContext.ItemInvoices
                .Include(i => i.Item)
                .Include(i => i.Invoice)
                .Include(i => i.Warranty)
                .Include(i => i.ReturnPolicy)
                .Where(ii => ii.InvoiceId == invoiceID)
                .Select(ii => ii.Item)
                .ToList();
            return itemsForInvoice;
        }
    }
}

