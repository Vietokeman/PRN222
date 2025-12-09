using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.Base;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFHRRequestSystem.Repositories.VietN
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository()
        {
            
        }
        public SystemUserAccountRepository(FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext context) => _context = context;
        public async Task<SystemUserAccount> GetUserAccount(string userName, string password)
        {
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(a => a.UserName == userName && a.Password == password && a.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(a => a.Email == userName && a.Password == password && a.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(a => a.Phone == userName && a.Password == password && a.IsActive == true);
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(a => a.EmployeeCode == userName && a.Password == password && a.IsActive == true);
        }
    }
}
