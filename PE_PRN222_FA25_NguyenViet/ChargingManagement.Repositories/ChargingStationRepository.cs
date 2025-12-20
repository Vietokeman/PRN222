using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories.Base;
using ChargingManagement.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingManagement.Repositories
{
    public class ChargingStationRepository : GenericRepository<ChargingStation>
    {
        public ChargingStationRepository()
        {
            
        }
        public ChargingStationRepository(FA25EChargingDBContext context) 
        {
            context = _context;
        }


    }
}
