using JewelleryShop.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess
{
    public interface IUnitOfWork
    {
        public IStaffRepository StaffRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        public IWarrantyRepository WarrantyRepository { get; }
        public IStaffStationRepository StaffStationRepository { get; }
        public IRewardsProgramRepository RewardsProgramRepository { get; }
        public ICollectionRepository CollectionRepository { get; }
        public IGemstoneRepository GemstoneRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
