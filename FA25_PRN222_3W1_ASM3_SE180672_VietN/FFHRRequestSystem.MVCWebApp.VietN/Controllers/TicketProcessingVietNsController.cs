using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FFHRRequestSystem.Entitites.VietN.Models;
using FFHRRequestSystem.Repositories.VietN.DBContext;
using FFHRRequestSystem.Services.VietN;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.AspNetCore.Authorization;

namespace FFHRRequestSystem.MVCWebApp.VietN.Controllers
{
    [Authorize]
    public class TicketProcessingVietNsController : Controller
    {
        //private readonly FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext _context;
        private readonly TicketProcessingVietNService _service;
        private readonly ProcessingTypeVietNService _processingTypeService;

        public TicketProcessingVietNsController(TicketProcessingVietNService service,
        ProcessingTypeVietNService processingTypeService)
        {
            _service = service;
            _processingTypeService = processingTypeService;
        }

        // GET: TicketProcessingVietNs
        public async Task<IActionResult> Index(string processingAction, string actionDescription, string typeName, int currentPage = 1, int pageSize = 10)
        {
            List<TicketProcessingVietN> items;
            
            if (!string.IsNullOrEmpty(processingAction) || !string.IsNullOrEmpty(actionDescription) || !string.IsNullOrEmpty(typeName))
            {
                items = await _service.SearchAsync(processingAction, actionDescription, typeName);
            }
            else
            {
                items = await _service.GetAllAsync();
            }

            ViewData["ProcessingAction"] = processingAction;
            ViewData["ActionDescription"] = actionDescription;
            ViewData["TypeName"] = typeName;

            // Pagination logic
            int totalItems = items.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            if (currentPage < 1) currentPage = 1;
            if (currentPage > totalPages && totalPages > 0) currentPage = totalPages;

            int fromEntry = totalItems == 0 ? 0 : (currentPage - 1) * pageSize + 1;
            int toEntry = Math.Min(currentPage * pageSize, totalItems);

            var pagedItems = items
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["CurrentPage"] = currentPage;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalItems"] = totalItems;
            ViewData["TotalPages"] = totalPages;
            ViewData["FromEntry"] = fromEntry;
            ViewData["ToEntry"] = toEntry;
            ViewData["PageSizeOptions"] = new[] { 5, 10, 25, 50, 100 };

            return View(pagedItems);
        }

        // GET: TicketProcessingVietNs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketProcessingVietN = await _service.GetByIdAsync(id.Value);
            if (ticketProcessingVietN == null)
            {
                return NotFound();
            }

            return View(ticketProcessingVietN);
        }

        // GET: TicketProcessingVietNs/Create
        public async Task<IActionResult> Create()
        {
            var listTypes = await _processingTypeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(listTypes, "ProcessingTypeVietNid", "TypeName");

            return View();
        }

        // POST: TicketProcessingVietNs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketProcessingVietN ticketProcessingVietN)
        {
            if (ModelState.IsValid)
            {
                // Ensure new Guid and set default values
                ticketProcessingVietN.TicketProcessingVietNid = Guid.NewGuid();
                ticketProcessingVietN.ProcessedBy = "VietN";
                ticketProcessingVietN.CreatedDate = DateTime.Now;
                ticketProcessingVietN.ModifiedDate = DateTime.Now;

                var result = await _service.CreateAsync(ticketProcessingVietN);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = $"Ticket '{ticketProcessingVietN.ProcessingCode}' created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to create ticket. Please try again.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid form data. Please check your input.";
            }

            var listTypes = await _processingTypeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(listTypes, "ProcessingTypeVietNid", "TypeName", ticketProcessingVietN.ProcessingTypeVietNid);
            return View(ticketProcessingVietN);
        }

        // GET: TicketProcessingVietNs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketProcessingVietN = await _service.GetByIdAsync(id.Value);
            if (ticketProcessingVietN == null)
            {
                return NotFound();
            }
            var listTypes = await _processingTypeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(listTypes, "ProcessingTypeVietNid", "TypeName", ticketProcessingVietN.ProcessingTypeVietNid);
            return View(ticketProcessingVietN);
        }

        // POST: TicketProcessingVietNs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TicketProcessingVietNid,ProcessingCode,TicketReference,ProcessingTypeVietNid,ProcessingAction,ActionDescription,RelatedTicketCode,PriorityLevel,Status,OverdueDays,EscalationLevel,IsAutoProcessed,ProcessedBy,ProcessedDate,ResolvedNote,CreatedDate,ModifiedDate")] TicketProcessingVietN ticketProcessingVietN)
        {
            if (id != ticketProcessingVietN.TicketProcessingVietNid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticketProcessingVietN.ModifiedDate = DateTime.Now;
                    var result = await _service.UpdateAsync(ticketProcessingVietN);
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = $"Ticket '{ticketProcessingVietN.ProcessingCode}' updated successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to update ticket. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                }
            }
            var listTypes = await _processingTypeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(listTypes, "ProcessingTypeVietNid", "TypeName", ticketProcessingVietN.ProcessingTypeVietNid);
            return View(ticketProcessingVietN);
        }

        // GET: TicketProcessingVietNs/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketProcessingVietN = await _service.GetByIdAsync(id);
            if (ticketProcessingVietN == null)
            {
                return NotFound();
            }

            return View(ticketProcessingVietN);
        }

        // POST: TicketProcessingVietNs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticket = await _service.GetByIdAsync(id);
            var ticketCode = ticket?.ProcessingCode ?? "Unknown";
            
            var result = await _service.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = $"Ticket '{ticketCode}' deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to delete ticket '{ticketCode}'.";
            }
            
            return RedirectToAction(nameof(Index));
        }


    }
}
