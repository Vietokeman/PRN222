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
            if (id == null)
            {
                return NotFound();
            }

            var result = _service.DeleteAsync(id.Value);
            if (result == null)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
