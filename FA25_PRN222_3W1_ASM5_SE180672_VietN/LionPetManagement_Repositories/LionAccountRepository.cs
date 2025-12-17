using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.Base;
using LionPetManagement_Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_Repositories
{
    public class LionAccountRepository : GenericRepository<LionAccount>
    {
        public LionAccountRepository()
        {
        }

        public LionAccountRepository(SU25LionDBContext context) => _context = context;

        public async Task<LionAccount> GetUserAccount(string email, string password)
        {
            return await _context.LionAccounts.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}
