﻿using AutoMapper;
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
        public InvoiceRepository(JewelleryDBContext dbcontext, IMapper mapper) : base(dbcontext)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<InvoiceWithItemsDTO> CreateInvoiceWithItemsAsync(Invoice invoice, IEnumerable<string> itemIds, string returnPolicyId, string warrantyId)
        {
            var itemIDAdded = new List<string>();
            foreach (var itemId in itemIds)
            {
                var itemInvoice = new ItemInvoice
                {
                    InvoiceId = invoice.Id,
                    ItemId = itemId,
                    WarrantyId = warrantyId
                };

                await _dbContext.ItemInvoices.AddAsync(itemInvoice);
                itemIDAdded.Add(itemId);
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

