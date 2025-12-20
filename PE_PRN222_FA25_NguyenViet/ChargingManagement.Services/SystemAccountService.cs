using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingManagement.Services
{
    public class SystemAccountService
    {
        private readonly SystemUserRepository _repo;
        public SystemAccountService()
        {
           _repo ??= new SystemUserRepository();
        }
        public async Task<SystemUser> GetAccount(string username, string password)
        {
            return await _repo.GetAccount(username, password);
        }

        public async Task<List<SystemUser>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
