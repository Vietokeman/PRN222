using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using FFHRRequestSystem.Services.VietN;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    public class DetailsModel : PageModel
    {
        private readonly TicketProcessingVietNService _service;

        public DetailsModel(TicketProcessingVietNService service)
        {
            _service = service;
        }

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
    }
}
