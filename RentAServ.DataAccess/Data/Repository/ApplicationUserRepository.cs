using RentAServ.DataAccess.Data.Repository.IRepository;
using RentAServ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository
{
    class ApplicationUserRepository:Repository<ApplicationUser>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void LockUser(string userId)
        {
            var userFromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id==userId);
            userFromdb.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnlockUser(string userId)
        {
            var userFromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            userFromdb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
