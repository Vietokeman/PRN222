using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using FFHRRequestSystem.Services.VietN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    [Authorize(Roles = "1, 3")]
    public class CreateModel : PageModel
    {
        private readonly TicketProcessingVietNService _ticketService;
        private readonly ProcessingTypeVietNService _typeService;

        public CreateModel(TicketProcessingVietNService ticketService, ProcessingTypeVietNService typeService)
        {
           _ticketService = ticketService;
              _typeService = typeService;
        }

        public async Task<IActionResult> OnGet()
        {
            var selectListItems = await _typeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(selectListItems, "ProcessingTypeVietNid", "TypeName");
            if (TicketProcessingVietN == null)
            {
                TicketProcessingVietN = new TicketProcessingVietN()
                {
                    TicketProcessingVietNid = Guid.NewGuid(),
                    PriorityLevel = 1, // Default to Low
                    Status = 1, // Default to Active
                    CreatedDate = DateTime.Now
                };
            }
            return Page();
        }

        [BindProperty]
        public TicketProcessingVietN TicketProcessingVietN { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Ensure new Guid for ID
                TicketProcessingVietN.TicketProcessingVietNid = Guid.NewGuid();

                // Set default dates
                TicketProcessingVietN.CreatedDate = DateTime.UtcNow;
                TicketProcessingVietN.ModifiedDate = DateTime.UtcNow;

                try
                {
                    var result = await _ticketService.CreateAsync(TicketProcessingVietN);
                    if (result > 0)
                    {
                        return RedirectToPage("./Index");
                    }
                }
                catch (Exception ex)
                {
                    // Log the detailed error
                    ModelState.AddModelError(string.Empty, $"Error creating record: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError(string.Empty, $"Inner exception: {ex.InnerException.Message}");
                    }
                    return Page();
                }
                var selectListItems = await _typeService.GetAllAsync();
                ViewData["ProcessingTypeVietNid"] = new SelectList(selectListItems, "ProcessingTypeVietNid", "TypeName", TicketProcessingVietN.ProcessingTypeVietNid);
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the ticket processing record.");
            return Page();
        }

        private bool ValidateTicketReference(string ticketReference)
        {
            if (string.IsNullOrEmpty(ticketReference)) return false;

            // Regex: starts with uppercase, followed by letters, numbers, spaces (for Title Case), no special chars
            var regex = new Regex(@"^[A-Z][a-zA-Z0-9\s]*$");
            return regex.IsMatch(ticketReference) && !ticketReference.Contains("@") && !ticketReference.Contains("#");
        }
    }
}
