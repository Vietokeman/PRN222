using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.Base;
using LionPetManagement_Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_Repositories
{
    public class LionTypeRepository : GenericRepository<LionType>
    {
        public LionTypeRepository()
        {
            
        }
        public LionTypeRepository(SU25LionDBContext context)
        {
            _context = context;
        }
    }
}
