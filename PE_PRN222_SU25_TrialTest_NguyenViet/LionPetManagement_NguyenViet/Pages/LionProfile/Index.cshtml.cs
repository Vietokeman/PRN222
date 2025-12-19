using LionPetManagement.Entities.Models;
using LionPetManagement.Repositories.DBContext;
using LionPetManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionPetManagement_NguyenViet.Pages.LionProfile
{
    [Authorize]

    public class IndexModel : PageModel
    {
        private readonly LionProfileService _service;
        private const int PageSize = 3;


        public IndexModel(LionProfileService service)
        {
            _service = service;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? SearchWeight { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchLionTypeName
        {
            get; set;
        }

        public IList<LionPetManagement.Entities.Models.LionProfile> LionProfile { get;set; } = default!;

        public async Task<JsonResult> OnGetRefreshListAsync()
        {
            var allData = await _service.GetAllAsync();
            var profiles = allData
                .OrderByDescending(x => x.LionProfileId)
                .Take(PageSize)
                .Select(p => new {
                    lionProfileId = p.LionProfileId,
                    lionName = p.LionName,
                    weight = p.Weight,
                    characteristics = p.Characteristics,
                    warning = p.Warning,
                    modifiedDate = p.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    lionTypeName = p.LionType?.LionTypeName ?? ""
                })
                .ToList();
            return new JsonResult(profiles);
        }
        public async Task OnGetAsync(int pageIndex = 1)
        {
            CurrentPage = pageIndex;

            List<LionPetManagement.Entities.Models.LionProfile> allData;

            if (SearchWeight.HasValue || !string.IsNullOrEmpty(SearchLionTypeName))
            {
                allData = await _service.SearchAsync(SearchWeight, SearchLionTypeName);
            }
            else
            {
                allData = await _service.GetAllAsync();
            }

            TotalPages = (int)Math.Ceiling(allData.Count / (double)PageSize);

            LionProfile = allData
                .OrderByDescending(x => x.LionProfileId)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
