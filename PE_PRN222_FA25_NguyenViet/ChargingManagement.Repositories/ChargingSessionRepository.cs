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
    public class ChargingSessionRepository :GenericRepository<ChargingSession>
    {
        public ChargingSessionRepository()
        {
            
        }
        public ChargingSessionRepository(FA25EChargingDBContext context) => _context = context;

        public async Task<List<ChargingSession>> GetAll()
        {
            return await _context.ChargingSessions.Include(c => c.Station).Include(c => c.Driver).ToListAsync();
        }

        public async Task<ChargingSession> GetById(int id)
        {
            return await _context.ChargingSessions.Include(c => c.Station).Include(c => c.Driver).FirstOrDefaultAsync(cs => cs.SessionId == id);
        }

        public async Task<List<ChargingSession>> Search()
        {
            return await _context.ChargingSessions.Include(c => c.Station).ToListAsync();
        }

        public async Task<List<ChargingSession>> SearchWithFilters(string stationName, decimal? minCost, decimal? maxCost, DateTime? startTimeFrom, DateTime? startTimeTo)
        {
            var query = _context.ChargingSessions
                .Include(c => c.Station)
                .Include(c => c.Driver)
                .AsQueryable();

            // Apply filters with AND logic
            if (!string.IsNullOrWhiteSpace(stationName))
            {
                query = query.Where(x => x.Station.StationName.Contains(stationName));
            }

            if (minCost.HasValue && maxCost.HasValue)
            {
                query = query.Where(x => x.Cost >= minCost.Value && x.Cost <= maxCost.Value);
            }
            else if (minCost.HasValue)
            {
                query = query.Where(x => x.Cost >= minCost.Value);
            }
            else if (maxCost.HasValue)
            {
                query = query.Where(x => x.Cost <= maxCost.Value);
            }

            if (startTimeFrom.HasValue && startTimeTo.HasValue)
            {
                var endOfDay = startTimeTo.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.StartTime >= startTimeFrom.Value && x.StartTime <= endOfDay);
            }
            else if (startTimeFrom.HasValue)
            {
                query = query.Where(x => x.StartTime >= startTimeFrom.Value);
            }
            else if (startTimeTo.HasValue)
            {
                var endOfDay = startTimeTo.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.StartTime <= endOfDay);
            }

            return await query
                .OrderBy(x => x.Station.StationName)
                .ToListAsync();
        }
    }
}
