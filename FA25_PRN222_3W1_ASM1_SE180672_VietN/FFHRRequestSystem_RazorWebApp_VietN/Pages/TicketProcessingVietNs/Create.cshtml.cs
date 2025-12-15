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
            // Custom validation
            if (!string.IsNullOrEmpty(TicketProcessingVietN.ProcessingCode))
            {
                var existingTicket = await _ticketService.GetAllAsync();
                if (existingTicket.Any(t => t.ProcessingCode.Equals(TicketProcessingVietN.ProcessingCode, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("TicketProcessingVietN.ProcessingCode", "Processing Code already exists. Please use a unique code.");
                }
            }

            if (!string.IsNullOrEmpty(TicketProcessingVietN.TicketReference) && !ValidateTicketReference(TicketProcessingVietN.TicketReference))
            {
                ModelState.AddModelError("TicketProcessingVietN.TicketReference", "Ticket Reference must start with an uppercase letter and contain only letters, numbers, and spaces.");
            }


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
                        TempData["SuccessMessage"] = $"Ticket '{TicketProcessingVietN.ProcessingCode}' created successfully!";
                        return RedirectToPage("./Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to create ticket. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    // Log the detailed error
                    TempData["ErrorMessage"] = $"Error creating ticket: {ex.Message}";
                    ModelState.AddModelError(string.Empty, $"Error creating record: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError(string.Empty, $"Inner exception: {ex.InnerException.Message}");
                    }
                }
                var selectListItems = await _typeService.GetAllAsync();
                ViewData["ProcessingTypeVietNid"] = new SelectList(selectListItems, "ProcessingTypeVietNid", "TypeName", TicketProcessingVietN.ProcessingTypeVietNid);
                return Page();
            }

            TempData["ErrorMessage"] = "Please correct the validation errors and try again.";
            var processingTypes = await _typeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(processingTypes, "ProcessingTypeVietNid", "TypeName", TicketProcessingVietN.ProcessingTypeVietNid);
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
