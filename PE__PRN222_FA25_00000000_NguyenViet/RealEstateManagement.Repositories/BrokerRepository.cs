using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories.Base;
using RealEstateManagement.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement.Repositories
{
    public class BrokerRepository : GenericRepository<Broker>
    {
        public BrokerRepository()
        {
            
        }
        public BrokerRepository(FA25RealEstateDBContext context)
        {
            _context = context;
        }
    }
}
