using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using FFHRRequestSystem.Services.VietN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    [Authorize(Roles = "1, 3")]
    public class DeleteModel : PageModel
    {
        private readonly TicketProcessingVietNService _service;

        public DeleteModel(TicketProcessingVietNService service)
        {
            _service = service;
        }

        [BindProperty]
        public TicketProcessingVietN TicketProcessingVietN { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketprocessingvietn = await _service.GetByIdAsync(id.Value);

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
            var deleteId = id ?? TicketProcessingVietN?.TicketProcessingVietNid;
            if (deleteId == null)
            {
                TempData["ErrorMessage"] = "❌ Invalid ticket ID.";
                return NotFound();
            }

            var ticketCode = TicketProcessingVietN?.ProcessingCode ?? "Unknown";
            var result = await _service.DeleteAsync(deleteId.Value);
            if (!result)
            {
                TempData["ErrorMessage"] = $"❌ Failed to delete ticket '{ticketCode}'.";
                return Page();
            }

            TempData["SuccessMessage"] = $"🗑️ Ticket '{ticketCode}' deleted successfully!";
            return RedirectToPage("./Index");
        }
    }
}
