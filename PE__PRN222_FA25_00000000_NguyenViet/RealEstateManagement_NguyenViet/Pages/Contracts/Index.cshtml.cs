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
    public class IndexModel : PageModel
    {
        private readonly ContractService _service;
        private const int PageSize = 3;

        public IndexModel(ContractService service)
        {
            _service = service;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IList<Contract> Contract { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchPropertyType { get; set; }
        [BindProperty(SupportsGet = true)]
        public int SearchSigningDate { get; set; }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            CurrentPage = pageIndex;

            List<Contract> allData;
            allData = await _service.GetAllAsync();

            TotalPages = (int)Math.Ceiling(allData.Count / (double)PageSize);

            Contract = allData
                .OrderByDescending(x => x.ContractId)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}