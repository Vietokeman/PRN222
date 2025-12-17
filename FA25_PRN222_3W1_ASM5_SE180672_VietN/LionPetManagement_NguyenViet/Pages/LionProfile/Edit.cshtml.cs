using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.DBContext;
using LionPetManagement_Services;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_NguyenViet.Pages.LionProfile
{
    [Authorize(Roles = "2")]
    public class EditModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;
        private readonly LionTypeService _lionTypeService;

        public EditModel(LionProfileService lionProfileService, LionTypeService lionTypeService)
        {
            _lionProfileService = lionProfileService;
            _lionTypeService = lionTypeService;
        }

        [BindProperty]
        public LionPetManagement_Entities.Models.LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _lionProfileService.GetByIdAsync(id.Value);
            if (lionprofile == null)
            {
                return NotFound();
            }
            LionProfile = lionprofile;
            
            var lionTypes = await _lionTypeService.GetAllAsync();
            ViewData["LionTypeId"] = new SelectList(lionTypes, "LionTypeId", "LionTypeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
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

            await _lionProfileService.UpdateAsync(LionProfile);

            return RedirectToPage("./Index");
        }
    }
}
