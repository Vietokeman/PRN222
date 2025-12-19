using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories.DBContext;
using LionPetManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_NguyenViet.Pages.LionProfile
{
    [Authorize(Roles = "2")]

    public class DeleteModel : PageModel
    {
        private readonly LionProfileService _service;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DeleteModel(LionProfileService service, IHubContext<NotificationHub> hubContext)
        {

            _service = service;
            _hubContext = hubContext;
        }


        [BindProperty]
        public LionPetManagement.Entities.Models.LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _service.GetByIdAsync(id.Value);

            if (lionprofile == null)
            {
                return NotFound();
            }
            else
            {
                LionProfile = lionprofile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _service.GetByIdAsync(id.Value);
            if (lionprofile != null)
            {
                await _service.DeleteAsync(lionprofile.LionProfileId);

                // Notify all clients via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveDelete", id.ToString());
            }

            return RedirectToPage("./Index");
        }
    }
}
