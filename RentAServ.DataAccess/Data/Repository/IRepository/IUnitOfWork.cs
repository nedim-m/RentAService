using System;
using System.Collections.Generic;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }
        IServiceRepository Service { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
