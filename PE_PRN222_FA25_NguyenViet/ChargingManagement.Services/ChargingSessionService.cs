using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingManagement.Services
{
    public class ChargingSessionService
    {
        private readonly ChargingSessionRepository _repo;
        public ChargingSessionService()
        {
            _repo ??= new ChargingSessionRepository();
        }
        public async Task<List<ChargingSession>> GetAllAsync()
        {
            return await _repo.GetAll();
        }
        public async Task<ChargingSession> GetByIdAsync(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<int> CreateAsync(ChargingSession item)
        {
            return await _repo.CreateAsync(item);
        }

        public async Task<int> UpdateAsync(ChargingSession item)
        {
            return await _repo.UpdateAsync(item);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item != null)
            {
                return await _repo.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<ChargingSession>> SearchAsync(string stationName, decimal? minCost, decimal? maxCost, DateTime? startTimeFrom, DateTime? startTimeTo)
        {
            return await _repo.SearchWithFilters(stationName, minCost, maxCost, startTimeFrom, startTimeTo);
        }
    }
}
