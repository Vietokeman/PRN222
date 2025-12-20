using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChargingManagement.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using ChargingManagement.Services;

namespace ChargingManagement_NguyenViet.Pages.ChargingSessions
{
    [Authorize(Roles = "1, 2")]
    public class CreateModel : PageModel
    {
        private readonly ChargingSessionService _sessionService;
        private readonly ChargingStationService _stationService;
        private readonly SystemAccountService _userService;

        public CreateModel(
            ChargingSessionService sessionService,
            ChargingStationService stationService,
            SystemAccountService userService)
        {
            _sessionService = sessionService;
            _stationService = stationService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userService.GetAllAsync();
            var stations = await _stationService.GetAllAsync();

            ViewData["DriverId"] = new SelectList(users, "UserId", "Username");
            ViewData["StationId"] = new SelectList(stations, "StationId", "StationName");
            return Page();
        }

        [BindProperty]
        public ChargingSession ChargingSession { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var users = await _userService.GetAllAsync();
                var stations = await _stationService.GetAllAsync();
                ViewData["DriverId"] = new SelectList(users, "UserId", "Username");
                ViewData["StationId"] = new SelectList(stations, "StationId", "StationName");
                return Page();
            }

            await _sessionService.CreateAsync(ChargingSession);

            return RedirectToPage("./Index");
        }
    }
}
