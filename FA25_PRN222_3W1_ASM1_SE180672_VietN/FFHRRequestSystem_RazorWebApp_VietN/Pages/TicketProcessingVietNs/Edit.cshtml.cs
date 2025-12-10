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
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "❌ Invalid form data. Please check your input.";
                return Page();
            }
            TicketProcessingVietN.ModifiedDate = DateTime.Now;

            // ✔ Không cho sửa CreatedDate
            ModelState.Remove("TicketProcessingVietN.CreatedDate");
            var result = await _service.UpdateAsync(TicketProcessingVietN);

            if (result > 0)
            {
                TempData["SuccessMessage"] = $"✅ Ticket '{TicketProcessingVietN.ProcessingCode}' updated successfully!";
                return RedirectToPage("./Index");
            }

            TempData["ErrorMessage"] = $"❌ Failed to update ticket '{TicketProcessingVietN.ProcessingCode}'.";
            var processingTypes = await _typeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(processingTypes, "ProcessingTypeVietNid", "TypeName");
            return Page();
        }

    }
}
