using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement.Services
{
    public class LionAccountService
    {
        private readonly LionAccountRepository _repo;
        public LionAccountService()
        {
            _repo ??= new LionAccountRepository();
        }

        public async Task<LionAccount> GetAccount(string email, string password)
        {
            return await _repo.GetAccount(email, password);
        }
    }
}
