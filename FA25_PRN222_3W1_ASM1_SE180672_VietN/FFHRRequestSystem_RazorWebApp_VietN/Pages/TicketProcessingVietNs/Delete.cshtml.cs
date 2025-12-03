using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    public class DeleteModel : PageModel
    {
        private readonly FFHRRequestSystem.Repositories.VietN.DBContext.FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext _context;

        public DeleteModel(FFHRRequestSystem.Repositories.VietN.DBContext.FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TicketProcessingVietN TicketProcessingVietN { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketprocessingvietn = await _context.TicketProcessingVietNs.FirstOrDefaultAsync(m => m.TicketProcessingVietNid == id);

            if (ticketprocessingvietn == null)
            {
                return NotFound();
            }
            else
            {
                TicketProcessingVietN = ticketprocessingvietn;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketprocessingvietn = await _context.TicketProcessingVietNs.FindAsync(id);
            if (ticketprocessingvietn != null)
            {
                TicketProcessingVietN = ticketprocessingvietn;
                _context.TicketProcessingVietNs.Remove(TicketProcessingVietN);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
