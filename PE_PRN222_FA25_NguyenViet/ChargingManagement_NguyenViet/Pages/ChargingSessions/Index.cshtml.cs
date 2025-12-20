using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChargingManagement.Entities.Models;
using ChargingManagement.Repositories.DBContext;
using Microsoft.AspNetCore.Authorization;
using ChargingManagement.Services;

namespace ChargingManagement_NguyenViet.Pages.ChargingSessions
{
    [Authorize(Roles = "1, 2, 3")]
    public class IndexModel : PageModel
    {
        private readonly ChargingSessionService _service;
        private const int PageSize = 7;

        public IndexModel(ChargingSessionService service)
        {
            _service = service;
        }
        
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchStationName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public decimal? SearchMinCost { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public decimal? SearchMaxCost { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? SearchStartTimeFrom { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? SearchStartTimeTo { get; set; }
        
        public IList<ChargingSession> ChargingSession { get; set; } = default!;
        
        // For aggregate report (requirement 6)
        public IList<IGrouping<string, ChargingSession>> GroupedSessions { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageKwhConsumed { get; set; }
        public bool HasSearchResults { get; set; }
        
        public async Task OnGetAsync(int pageIndex = 1)
        {
            CurrentPage = pageIndex;

            List<ChargingSession> allData;

            // Check if any search parameter is provided
            if (!string.IsNullOrWhiteSpace(SearchStationName) || 
                SearchMinCost.HasValue || 
                SearchMaxCost.HasValue || 
                SearchStartTimeFrom.HasValue || 
                SearchStartTimeTo.HasValue)
            {
                // Search mode: Get filtered results and group by StationName
                allData = await _service.SearchAsync(SearchStationName, SearchMinCost, SearchMaxCost, SearchStartTimeFrom, SearchStartTimeTo);
                
                if (allData.Any())
                {
                    HasSearchResults = true;
                    TotalRevenue = allData.Sum(x => x.Cost);
                    AverageKwhConsumed = allData.Average(x => x.KwhConsumed);
                    
                    // Group by StationName (results already sorted by StationName in repository)
                    GroupedSessions = allData.GroupBy(x => x.Station.StationName).ToList();
                }
            }
            else
            {
                // Normal mode: Get all with pagination
                allData = await _service.GetAllAsync();
            }

            TotalPages = (int)Math.Ceiling(allData.Count / (double)PageSize);

            ChargingSession = allData
                .OrderByDescending(x => x.StartTime)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}