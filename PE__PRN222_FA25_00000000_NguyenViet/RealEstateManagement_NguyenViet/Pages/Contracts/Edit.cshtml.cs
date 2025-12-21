using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    [Authorize(Roles = "2")]

    public class EditModel : PageModel
    {
        private readonly ContractService _service;
        private readonly BrokerService _brokerService;


        public EditModel(ContractService service, BrokerService brokerService)
        {
            _service = service;
            _brokerService = brokerService;
        }

        [BindProperty]
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
            Contract = contract;
            var brokers = await _brokerService.GetAllAsync();
            ViewData["BrokerId"] = new SelectList(brokers, "BrokerId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var brokers = await _brokerService.GetAllAsync();
                ViewData["BrokerId"] = new SelectList(brokers, "BrokerId", "FullName");
                return Page();
            }

            await _service.UpdateAsync(Contract);


            return RedirectToPage("./Index");
        }

    }
}
