using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories.Base;
using LionPetManagement.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement.Repositories
{
    public  class LionTypeRepository: GenericRepository<LionType>
    {
        public LionTypeRepository()
        {
            
        }
        public LionTypeRepository(SU25LionDBContext context) => _context = context;
    }
}
