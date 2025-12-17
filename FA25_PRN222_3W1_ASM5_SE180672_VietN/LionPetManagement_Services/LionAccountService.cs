using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_Services
{
    public class LionAccountService
    {
        private readonly LionAccountRepository _repo;
        public LionAccountService()
        {
            _repo ??= new LionAccountRepository();
        }

        public async Task<LionAccount> GetUserAccount(string email, string password)
        {
            return await _repo.GetUserAccount(email, password);
        }
    }
}
