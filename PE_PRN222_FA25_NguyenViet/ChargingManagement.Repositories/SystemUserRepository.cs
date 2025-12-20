using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories.Base;
using ChargingManagement.Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingManagement.Repositories
{
    public class SystemUserRepository : GenericRepository<SystemUser>
    {
        public SystemUserRepository()
        {
            
        }
        public SystemUserRepository(FA25EChargingDBContext context) => _context = context;

        public async Task<SystemUser> GetAccount(string username, string password)
        {
            return await _context.SystemUsers.FirstOrDefaultAsync(a => a.Username == username && a.UserPassword == password);
        }

        public async Task<List<SystemUser>> GetAllAsync()
        {
            return await _context.SystemUsers.ToListAsync();
        }
    }
}
