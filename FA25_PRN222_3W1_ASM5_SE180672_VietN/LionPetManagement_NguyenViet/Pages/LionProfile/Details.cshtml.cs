using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_Entities.Models;
using LionPetManagement_Repositories.DBContext;
using LionPetManagement_Services;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_NguyenViet.Pages.LionProfile
{
    [Authorize(Roles = "2, 3")]
    public class DetailsModel : PageModel
    {
        private readonly LionProfileService _service;

        public DetailsModel(LionProfileService service)
        {
            _service = service;
        }

        public LionPetManagement_Entities.Models.LionProfile LionProfile { get; set; } = default!;

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
            else
            {
                LionProfile = lionprofile;
            }
            return Page();
        }
    }
}
