using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.DBContext;
using LionPetManagement_Services;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_NguyenViet.Pages.LionProfile
{
    [Authorize(Roles = "2")]
    public class CreateModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;
        private readonly LionTypeService _lionTypeService;

        public CreateModel(LionProfileService lionProfileService, LionTypeService lionTypeService)
        {
            _lionProfileService = lionProfileService;
            _lionTypeService = lionTypeService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var lionTypes = await _lionTypeService.GetAllAsync();
            ViewData["LionTypeId"] = new SelectList(lionTypes, "LionTypeId", "LionTypeName");
            return Page();
        }

        [BindProperty]
        public LionPetManagement_Entities.Models.LionProfile LionProfile { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var lionTypes = await _lionTypeService.GetAllAsync();
                ViewData["LionTypeId"] = new SelectList(lionTypes, "LionTypeId", "LionTypeName");
                return Page();
            }

            // Set ModifiedDate to current date/time
            LionProfile.ModifiedDate = DateTime.Now;

            await _lionProfileService.CreateAsync(LionProfile);

            return RedirectToPage("./Index");
        }
    }
}
