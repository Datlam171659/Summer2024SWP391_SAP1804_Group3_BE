using AutoMapper;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.dto;
using JewelleryShop.DataAccess.Models.ViewModel.InvoiceViewModel;
using JewelleryShop.DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        private readonly IWarrantyRepository _warrantyRepository;
        public InvoiceRepository(JewelleryDBContext dbcontext, IMapper mapper, IItemRepository itemRepository, IWarrantyRepository warrantyRepository) : base(dbcontext)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
            _itemRepository = itemRepository;
            _warrantyRepository = warrantyRepository;
        }

        public async Task<InvoiceCreateWithItemsDTO> CreateInvoiceWithItemsAsync(Invoice invoice, IEnumerable<InvoiceInputItemDTO> items, string returnPolicyId)
        {
            var itemAdded = new List<InvoiceInputItemDTO>();
            int invoiceQuantity = 0;
            foreach (var _item in items)
            {
                Warranty _warranty = new()
                {
                    CustomerId = invoice.CustomerId,
                    CreatedDate = DateTime.Now,
                    ExpiryDate = _item.warrantyExpiryDate
                };
                await _warrantyRepository.AddWarranty(_warranty);
                
                var itemInvoice = new ItemInvoice
                {
                    InvoiceId = invoice.Id,
                    ItemId = _item.itemID,
                    WarrantyId = _warranty.WarrantyId,
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
                itemAdded.Add(_item);
                Interlocked.Add(ref invoiceQuantity, 1); // 4 safety
            }

            invoice.Quantity = invoiceQuantity;
            await _dbContext.Invoices.AddAsync(invoice);
            InvoiceCreateWithItemsDTO invoiceWithItems = new InvoiceCreateWithItemsDTO
            {
                invoiceDTO = _mapper.Map<InvoiceInputNewDTO>(invoice),
                items = itemAdded,
                returnPolicyId = returnPolicyId
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

