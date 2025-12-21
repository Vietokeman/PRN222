using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateManagement.Entities.Models;
using RealEstateManagement.Repositories.DBContext;
using RealEstateManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManagement_NguyenViet.Pages.Contracts
{
    [Authorize(Roles = "2")]

    public class CreateModel : PageModel
    {
        private readonly ContractService _service;
        private readonly BrokerService _brokerService;


        public CreateModel(ContractService service, BrokerService brokerService)
        {
            _service = service;
            _brokerService = brokerService;
        }

        public async Task<IActionResult> OnGet()
        {
            var brokers = await _brokerService.GetAllAsync();
        ViewData["BrokerId"] = new SelectList(brokers, "BrokerId", "FullName");
            return Page();
        }

        [BindProperty]
        public Contract Contract { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var brokers = await _brokerService.GetAllAsync();
                ViewData["BrokerId"] = new SelectList(brokers, "BrokerId", "FullName");
                return Page();
            }


            await _service.CreateAsync(Contract);

            return RedirectToPage("./Index");
        }
    }
}
