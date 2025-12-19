using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories.DBContext;
using LionPetManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_NguyenViet.Pages.LionProfile
{
    [Authorize(Roles = "2")]

    public class CreateModel : PageModel
    {
        private readonly LionProfileService _service;
        private readonly LionTypeService _typeService;

        public CreateModel(LionProfileService service, LionTypeService typeService)
        {
            _service = service;
            _typeService = typeService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var allTypes = await _typeService.GetAllAsync();
            ViewData["LionTypeId"] = new SelectList(allTypes, "LionTypeId", "LionTypeName");
            return Page();
        }

        [BindProperty]
        public LionPetManagement.Entities.Models.LionProfile LionProfile { get; set; } = default!;

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

            await _service.CreateAsync(LionProfile);

            return RedirectToPage("./Index");
        }
    }
}
