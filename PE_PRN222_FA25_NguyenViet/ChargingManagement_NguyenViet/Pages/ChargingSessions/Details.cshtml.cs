using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories.DBContext;
using ChargingManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChargingManagement_NguyenViet.Pages.ChargingSessions
{
    public class DetailsModel : PageModel
    {
        private readonly ChargingSessionService _service;

        public DetailsModel(ChargingSessionService service)
        {
            _service = service;
        }

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
    }
}
