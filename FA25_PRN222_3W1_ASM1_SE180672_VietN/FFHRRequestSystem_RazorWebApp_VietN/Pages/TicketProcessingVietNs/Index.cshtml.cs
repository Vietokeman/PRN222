using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Services.VietN;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    public class IndexModel : PageModel
    {
        private readonly TicketProcessingVietNService _service;
        public IndexModel(TicketProcessingVietNService service)
        {
            _service = service;
        }
        public IList<TicketProcessingVietN> TicketProcessingVietN { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string ProcessingAction { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ActionDescription { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TypeName { get; set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(ProcessingAction) || !string.IsNullOrEmpty(ActionDescription) || !string.IsNullOrEmpty(TypeName))
            {
                TicketProcessingVietN = await _service.SearchAsync(ProcessingAction, ActionDescription, TypeName);
            }
            else
            {
                TicketProcessingVietN = await _service.GetAllAsync();
            }
        }
    }
}
