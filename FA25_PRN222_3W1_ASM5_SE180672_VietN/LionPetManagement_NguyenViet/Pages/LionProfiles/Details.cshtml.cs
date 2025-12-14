using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.DBContext;

namespace LionPetManagement_NguyenViet.Pages.LionProfiles
{
    public class DetailsModel : PageModel
    {
        private readonly LionPetManagement_Repositories.DBContext.SU25LionDBContext _context;

        public DetailsModel(LionPetManagement_Repositories.DBContext.SU25LionDBContext context)
        {
            _context = context;
        }

        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _context.LionProfiles.FirstOrDefaultAsync(m => m.LionProfileId == id);
            if (lionprofile == null)
            {
                return NotFound();
            }
            else
            {
                LionProfile = lionprofile;
            }
            return Page();
        }
    }
}
