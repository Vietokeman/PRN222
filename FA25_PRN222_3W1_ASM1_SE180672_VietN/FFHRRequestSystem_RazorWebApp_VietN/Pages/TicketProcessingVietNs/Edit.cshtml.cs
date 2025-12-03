using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    public class EditModel : PageModel
    {
        private readonly FFHRRequestSystem.Repositories.VietN.DBContext.FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext _context;

        public EditModel(FFHRRequestSystem.Repositories.VietN.DBContext.FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext context)
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

            var ticketprocessingvietn =  await _context.TicketProcessingVietNs.FirstOrDefaultAsync(m => m.TicketProcessingVietNid == id);
            if (ticketprocessingvietn == null)
            {
                return NotFound();
            }
            TicketProcessingVietN = ticketprocessingvietn;
           ViewData["ProcessingTypeVietNid"] = new SelectList(_context.ProcessingTypeVietNs, "ProcessingTypeVietNid", "TypeCode");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TicketProcessingVietN).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketProcessingVietNExists(TicketProcessingVietN.TicketProcessingVietNid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TicketProcessingVietNExists(Guid id)
        {
            return _context.TicketProcessingVietNs.Any(e => e.TicketProcessingVietNid == id);
        }
    }
}
