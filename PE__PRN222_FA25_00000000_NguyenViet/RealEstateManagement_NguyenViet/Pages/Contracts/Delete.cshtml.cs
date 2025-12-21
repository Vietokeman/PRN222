using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
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

    public class DeleteModel : PageModel
    {
        private readonly ContractService _service;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DeleteModel(ContractService service, IHubContext<NotificationHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
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
            else
            {
                Contract = contract;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _service.GetByIdAsync(id.Value);
            if (contract != null)
            {
                await _service.DeleteAsync(contract.ContractId);
                await _hubContext.Clients.All.SendAsync("ReceiveDelete", id.ToString());
            }

            return RedirectToPage("./Index");
        }
    }
}
