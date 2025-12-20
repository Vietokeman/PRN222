using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingManagement.Services
{
    public class ChargingStationService
    {
        private readonly ChargingStationRepository _repo;
        public ChargingStationService()
        {
            _repo ??= new ChargingStationRepository();
        }
        public async Task<List<ChargingStation>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
