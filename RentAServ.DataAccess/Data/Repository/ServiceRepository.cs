using RentAServ.DataAccess.Data.Repository.IRepository;
using RentAServ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository
{
   public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Service service)
        {
            var objFromDb = _db.Service.FirstOrDefault(s => s.Id == service.Id);
            objFromDb.Name = service.Name;
            objFromDb.Price = service.Price;
            objFromDb.ImageUrl = service.ImageUrl;
            objFromDb.LongDesc = service.LongDesc;
            objFromDb.FrequencyId = service.FrequencyId;
            objFromDb.CategoryId = service.CategoryId;

            _db.SaveChanges();


        }
    }
}
