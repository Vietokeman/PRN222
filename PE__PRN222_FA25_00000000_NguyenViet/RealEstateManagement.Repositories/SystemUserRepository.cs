using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories.Base;
using RealEstateManagement.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement.Repositories
{
    public class SystemUserRepository : GenericRepository<SystemUser>
    {
        public SystemUserRepository()
        {
            
        }
        public SystemUserRepository(FA25RealEstateDBContext context)
        {
            _context = context;
        }
        public async Task<SystemUser> GetAccount(string username, string password)
        {
            return await _context.SystemUsers.FirstOrDefaultAsync(a => a.Username == username && a.UserPassword == password);
        }
    }
}
