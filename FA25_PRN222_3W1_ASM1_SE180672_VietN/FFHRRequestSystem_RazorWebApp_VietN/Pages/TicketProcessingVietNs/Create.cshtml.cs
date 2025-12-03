using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using FFHRRequestSystem.Services.VietN;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
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
                // Set default dates
                TicketProcessingVietN.CreatedDate = DateTime.Now;
                TicketProcessingVietN.ModifiedDate = DateTime.Now;

                var result = await _ticketService.CreateAsync(TicketProcessingVietN);
                if (result > 0)
                {
                    return RedirectToPage("./Index");
                }
                var selectListItems = await _typeService.GetAllAsync();
                ViewData["ProcessingTypeVietNid"] = new SelectList(selectListItems, "ProcessingTypeVietNid", "TypeName", TicketProcessingVietN.ProcessingTypeVietNid);
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the ticket processing record.");
            return Page();
        }
    }
}
