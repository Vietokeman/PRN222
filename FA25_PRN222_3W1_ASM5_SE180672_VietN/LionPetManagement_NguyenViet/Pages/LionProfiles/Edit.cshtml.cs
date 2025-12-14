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

namespace LionPetManagement_NguyenViet.Pages.LionProfiles
{
    public class EditModel : PageModel
    {
        private readonly LionPetManagement_Repositories.DBContext.SU25LionDBContext _context;

        public EditModel(LionPetManagement_Repositories.DBContext.SU25LionDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile =  await _context.LionProfiles.FirstOrDefaultAsync(m => m.LionProfileId == id);
            if (lionprofile == null)
            {
                return NotFound();
            }
            LionProfile = lionprofile;
           ViewData["LionTypeId"] = new SelectList(_context.LionTypes, "LionTypeId", "LionTypeId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LionProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LionProfileExists(LionProfile.LionProfileId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LionProfileExists(int id)
        {
            return _context.LionProfiles.Any(e => e.LionProfileId == id);
        }
    }
}
