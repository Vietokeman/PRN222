using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement.Services
{
    public class SystemUserService
    {
        private readonly SystemUserRepository _repo;
        public SystemUserService()
        {
            _repo ??= new SystemUserRepository();
        }
        public async Task<SystemUser> GetAccount(string username, string password)
        {
            return await _repo.GetAccount(username, password);

        }
    }
}
