using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChargingManagement.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using ChargingManagement.Services;

namespace ChargingManagement_NguyenViet.Pages.ChargingSessions
{
    [Authorize(Roles = "1, 2")]
    public class EditModel : PageModel
    {
        private readonly ChargingSessionService _sessionService;
        private readonly ChargingStationService _stationService;
        private readonly SystemAccountService _userService;
        private DateTime _originalStartTime;
        private DateTime? _originalEndTime;

        public EditModel(
            ChargingSessionService sessionService,
            ChargingStationService stationService,
            SystemAccountService userService)
        {
            _sessionService = sessionService;
            _stationService = stationService;
            _userService = userService;
        }

        [BindProperty]
        public ChargingSession ChargingSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chargingsession = await _sessionService.GetByIdAsync(id.Value);
            if (chargingsession == null)
            {
                return NotFound();
            }
            ChargingSession = chargingsession;

            var users = await _userService.GetAllAsync();
            var stations = await _stationService.GetAllAsync();
            ViewData["DriverId"] = new SelectList(users, "UserId", "Username");
            ViewData["StationId"] = new SelectList(stations, "StationId", "StationName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Get original values for 8-hour check
            var originalSession = await _sessionService.GetByIdAsync(ChargingSession.SessionId);

            if (originalSession != null)
            {
                _originalStartTime = originalSession.StartTime;
                _originalEndTime = originalSession.EndTime;
            }

            // Advanced Requirement: Check 8-hour duration limit when updating StartTime or EndTime
            if (originalSession != null && ChargingSession.EndTime.HasValue &&
                (ChargingSession.StartTime != _originalStartTime || ChargingSession.EndTime != _originalEndTime))
            {
                var duration = ChargingSession.EndTime.Value - ChargingSession.StartTime;
                if (duration.TotalMinutes > 480) // 8 hours = 480 minutes
                {
                    ModelState.AddModelError("ChargingSession.EndTime",
                        "The total duration of the session must not exceed 8 hours (480 minutes).");
                }
            }

            if (!ModelState.IsValid)
            {
                var users = await _userService.GetAllAsync();
                var stations = await _stationService.GetAllAsync();
                ViewData["DriverId"] = new SelectList(users, "UserId", "Username");
                ViewData["StationId"] = new SelectList(stations, "StationId", "StationName");
                return Page();
            }

            try
            {
                await _sessionService.UpdateAsync(ChargingSession);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _sessionService.GetByIdAsync(ChargingSession.SessionId);
                if (exists == null)
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
    }
}
