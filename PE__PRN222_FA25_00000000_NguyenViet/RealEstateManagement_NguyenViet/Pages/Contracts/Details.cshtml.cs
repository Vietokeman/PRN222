using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories.DBContext;
using RealEstateManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManagement_NguyenViet.Pages.Contracts
{
    [Authorize(Roles = "2, 3")]
    public class DetailsModel : PageModel
    {
        private readonly ContractService _service;

        public DetailsModel(ContractService service)
        {
            _service = service;
        }

        public Contract Contract { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _service.GetByIdAsync(id.Value);
            if (contract == null)
            {
                return NotFound();
            }
            else
            {
                Contract = contract;
            }
            return Page();
        }
    }
}
