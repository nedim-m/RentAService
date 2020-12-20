using RentAServ.DataAccess.Data.Repository.IRepository;
using RentAServ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository
{
    class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
    }
}
