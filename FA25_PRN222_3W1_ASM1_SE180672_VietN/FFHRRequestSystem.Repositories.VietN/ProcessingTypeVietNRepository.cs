using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.Base;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFHRRequestSystem.Repositories.VietN
{
    public class ProcessingTypeVietNRepository : GenericRepository<ProcessingTypeVietN>
    {
        public ProcessingTypeVietNRepository()
        {
            
        }
        public ProcessingTypeVietNRepository(FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext context) => _context = context;
    }
}
