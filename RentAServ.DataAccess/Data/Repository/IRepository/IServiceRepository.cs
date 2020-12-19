using RentAServ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository.IRepository
{
   public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service service);
    }
}
