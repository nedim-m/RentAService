﻿using RentAServ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository.IRepository
{
   public interface IApplicationUserRepository:IRepository<ApplicationUser>
    {
        void LockUser(string userId);

        void UnlockUser(string userId);
    }
}
