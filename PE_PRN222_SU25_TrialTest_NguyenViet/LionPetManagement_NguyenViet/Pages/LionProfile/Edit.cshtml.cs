using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories.DBContext;
using LionPetManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_NguyenViet.Pages.LionProfile
{
    [Authorize(Roles = "2")]

    public class EditModel : PageModel
    {
        private readonly LionProfileService _service;
        private readonly LionTypeService _typeService;

        public EditModel(LionProfileService service, LionTypeService typeService)
        {
            _service = service;
            _typeService = typeService;
        }

        [BindProperty]
        public LionPetManagement.Entities.Models.LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _service.GetByIdAsync(id.Value);
            if (lionprofile == null)
            {
                return NotFound();
            }
            LionProfile = lionprofile;
            
            var allTypes = await _typeService.GetAllAsync();
            ViewData["LionTypeId"] = new SelectList(allTypes, "LionTypeId", "LionTypeName");
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var lionTypes = await _typeService.GetAllAsync();
                ViewData["LionTypeId"] = new SelectList(lionTypes, "LionTypeId", "LionTypeName");
                return Page();
            }

            LionProfile.ModifiedDate = DateTime.Now;

            await _service.UpdateAsync(LionProfile);

            return RedirectToPage("./Index");
        }
    }
}
