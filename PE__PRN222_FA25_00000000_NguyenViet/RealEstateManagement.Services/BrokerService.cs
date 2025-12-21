using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement.Services
{
    public class BrokerService
    {
        private readonly BrokerRepository _repo;
        public BrokerService()
        {
            _repo = new BrokerRepository();
        }

        public async Task<List<Broker>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
