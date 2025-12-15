using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using FFHRRequestSystem.Services.VietN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    [Authorize(Roles ="1, 3")]

    public class EditModel : PageModel
    {
        private readonly TicketProcessingVietNService _service;
        private readonly ProcessingTypeVietNService _typeService;

        public EditModel(TicketProcessingVietNService service, ProcessingTypeVietNService typeService)
        {
            _service = service;
            _typeService = typeService;
        }

        [BindProperty]
        public TicketProcessingVietN TicketProcessingVietN { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketprocessingvietn =  await _service.GetByIdAsync(id.Value);
            if (ticketprocessingvietn == null)
            {
                return NotFound();
            }
            TicketProcessingVietN = ticketprocessingvietn;
            var selectListItems = await _typeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(selectListItems, "ProcessingTypeVietNid", "TypeName"); return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Custom validation
            if (!string.IsNullOrEmpty(TicketProcessingVietN.ProcessingCode))
            {
                var existingTickets = await _service.GetAllAsync();
                if (existingTickets.Any(t => t.ProcessingCode.Equals(TicketProcessingVietN.ProcessingCode, StringComparison.OrdinalIgnoreCase) 
                    && t.TicketProcessingVietNid != TicketProcessingVietN.TicketProcessingVietNid))
                {
                    ModelState.AddModelError("TicketProcessingVietN.ProcessingCode", "Processing Code already exists. Please use a unique code.");
                }
            }

            if (!string.IsNullOrEmpty(TicketProcessingVietN.TicketReference) && !ValidateTicketReference(TicketProcessingVietN.TicketReference))
            {
                ModelState.AddModelError("TicketProcessingVietN.TicketReference", "Ticket Reference must start with an uppercase letter and contain only letters, numbers, and spaces.");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the validation errors and try again.";
                var processingTypes = await _typeService.GetAllAsync();
                ViewData["ProcessingTypeVietNid"] = new SelectList(processingTypes, "ProcessingTypeVietNid", "TypeName", TicketProcessingVietN.ProcessingTypeVietNid);
                return Page();
            }
            
            TicketProcessingVietN.ModifiedDate = DateTime.Now;

            // ✔ Không cho sửa CreatedDate
            ModelState.Remove("TicketProcessingVietN.CreatedDate");
            var result = await _service.UpdateAsync(TicketProcessingVietN);

            if (result > 0)
            {
                TempData["SuccessMessage"] = $"Ticket '{TicketProcessingVietN.ProcessingCode}' updated successfully!";
                return RedirectToPage("./Index");
            }

            TempData["ErrorMessage"] = $"Failed to update ticket '{TicketProcessingVietN.ProcessingCode}'.";
            var processingTypesForError = await _typeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(processingTypesForError, "ProcessingTypeVietNid", "TypeName", TicketProcessingVietN.ProcessingTypeVietNid);
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
