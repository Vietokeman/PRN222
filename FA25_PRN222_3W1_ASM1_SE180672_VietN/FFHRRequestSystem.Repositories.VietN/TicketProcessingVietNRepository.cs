using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.Base;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFHRRequestSystem.Repositories.VietN
{
    public class TicketProcessingVietNRepository : GenericRepository<TicketProcessingVietN>
    {
        public TicketProcessingVietNRepository()
        {
            
        }
        public TicketProcessingVietNRepository(FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext context) => _context= context;

        public async Task<List<TicketProcessingVietN>> GetAll()
        {
            try
            {
                return await _context.TicketProcessingVietNs.Include(c => c.TicketProcessingVietNid)
                    .OrderByDescending(c => c.TicketProcessingVietNid).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<TicketProcessingVietN> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.TicketProcessingVietNs.Include(a => a.ProcessingTypeVietN).FirstOrDefaultAsync(t => t.TicketProcessingVietNid == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<TicketProcessingVietN>> SearchAsync(string processingAction, string ActionDescription, string TypeName)
        {
            try
            {
                return await _context.TicketProcessingVietNs
                     //.Where(c => c.Title.Contains(tilte) 
                     //&& c.Description.Contains(description)
                     //&& c.TotalTicket == totalTicket)
                     //.OrderByDescending(c => c.ReportRecordLinhNbid)
                     //.ToListAsync();
                     .Include(c => c.TicketProcessingVietNid)
                     .Where(c => (string.IsNullOrEmpty(processingAction) || c.ProcessingAction.Contains(processingAction)
                     )
                     ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
