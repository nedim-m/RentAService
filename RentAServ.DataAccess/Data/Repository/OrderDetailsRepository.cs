using RentAServ.DataAccess.Data.Repository.IRepository;
using RentAServ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository
{
    class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
    }
}
