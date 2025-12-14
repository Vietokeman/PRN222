using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_Repositories
{
    public class LionRepository : GenericRepository<LionAccount>
    {
        public LionRepository() : base()
        {
        }
    }
}
