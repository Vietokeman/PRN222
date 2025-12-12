using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Services.VietN;
using Microsoft.AspNetCore.Authorization;

namespace FFHRRequestSystem_RazorWebApp_VietN.Pages.TicketProcessingVietNs
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TicketProcessingVietNService _service;
        public IndexModel(TicketProcessingVietNService service)
        {
            _service = service;
        }
        public IList<TicketProcessingVietN> TicketProcessingVietN { get;set; } = default!;
        public IList<TicketProcessingVietN> PagedTickets { get; set; } = new List<TicketProcessingVietN>();

        [BindProperty(SupportsGet = true)]
        public string ProcessingAction { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ActionDescription { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TypeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int FromEntry { get; set; }
        public int ToEntry { get; set; }
        public int[] PageSizeOptions { get; set; } = new[] { 5, 10, 25, 50, 100 };

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

            // Pagination logic
            TotalItems = TicketProcessingVietN.Count;
            TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
            
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

            FromEntry = TotalItems == 0 ? 0 : (CurrentPage - 1) * PageSize + 1;
            ToEntry = Math.Min(CurrentPage * PageSize, TotalItems);

            PagedTickets = TicketProcessingVietN
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public List<int> GetPageNumbers()
        {
            var pages = new List<int>();
            var maxPagesToShow = 5;
            var halfRange = maxPagesToShow / 2;

            var startPage = Math.Max(1, CurrentPage - halfRange);
            var endPage = Math.Min(TotalPages, startPage + maxPagesToShow - 1);

            if (endPage - startPage < maxPagesToShow - 1)
            {
                startPage = Math.Max(1, endPage - maxPagesToShow + 1);
            }

            for (int i = startPage; i <= endPage; i++)
            {
                pages.Add(i);
            }

            return pages;
        }
    }
}
