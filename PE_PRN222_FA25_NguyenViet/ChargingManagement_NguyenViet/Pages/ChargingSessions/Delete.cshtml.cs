using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories.DBContext;
using Microsoft.AspNetCore.Authorization;
using ChargingManagement.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChargingManagement_NguyenViet.Pages.ChargingSessions
{
    [Authorize(Roles = "1, 2")]
    public class DeleteModel : PageModel
    {
        private readonly ChargingSessionService _service;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DeleteModel(ChargingSessionService service, IHubContext<NotificationHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }

        [BindProperty]
        public ChargingSession ChargingSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chargingsession = await _service.GetByIdAsync(id.Value);

            if (chargingsession == null)
            {
                return NotFound();
            }
            else
            {
                ChargingSession = chargingsession;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleted = await _service.DeleteAsync(id.Value);
            
            if (deleted)
            {
                // Send SignalR notification to update table in real-time
                await _hubContext.Clients.All.SendAsync("ReceiveDelete", id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
