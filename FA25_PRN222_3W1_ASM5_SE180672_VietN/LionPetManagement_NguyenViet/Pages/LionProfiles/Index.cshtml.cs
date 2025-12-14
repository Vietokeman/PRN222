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
    public class IndexModel : PageModel
    {
        private readonly LionPetManagement_Repositories.DBContext.SU25LionDBContext _context;

        public IndexModel(LionPetManagement_Repositories.DBContext.SU25LionDBContext context)
        {
            _context = context;
        }

        public IList<LionProfile> LionProfile { get;set; } = default!;

        public async Task OnGetAsync()
        {
            LionProfile = await _context.LionProfiles
                .Include(l => l.LionType).ToListAsync();
        }
    }
}
