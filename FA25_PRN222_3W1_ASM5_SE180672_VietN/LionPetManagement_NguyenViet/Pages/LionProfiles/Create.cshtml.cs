using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.DBContext;

namespace LionPetManagement_NguyenViet.Pages.LionProfiles
{
    public class CreateModel : PageModel
    {
        private readonly LionPetManagement_Repositories.DBContext.SU25LionDBContext _context;

        public CreateModel(LionPetManagement_Repositories.DBContext.SU25LionDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LionTypeId"] = new SelectList(_context.LionTypes, "LionTypeId", "LionTypeId");
            return Page();
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LionProfiles.Add(LionProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
