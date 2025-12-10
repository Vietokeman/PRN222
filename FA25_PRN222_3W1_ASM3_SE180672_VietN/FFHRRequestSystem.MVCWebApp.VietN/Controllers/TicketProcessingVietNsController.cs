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

namespace FFHRRequestSystem.MVCWebApp.VietN.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
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
            ViewData["ProcessingTypeVietNid"] = new SelectList(listTypes, "ProcessingTypeVietNid", "TypeCode");

            //set default values
            var item = new TicketProcessingVietN()
            {
                TicketProcessingVietNid = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                ProcessedBy = "VietN",

            };
            return View();
        }

        // POST: TicketProcessingVietNs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( TicketProcessingVietN ticketProcessingVietN)
        {
            if (ModelState.IsValid)
            {
               var result =  await _service.CreateAsync(ticketProcessingVietN);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var listTypes = await _processingTypeService.GetAllAsync();
            ViewData["ProcessingTypeVietNid"] = new SelectList(listTypes, "ProcessingTypeVietNid", "TypeCode");
            return View(ticketProcessingVietN);
        }

        //// GET: TicketProcessingVietNs/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticketProcessingVietN = await _context.TicketProcessingVietNs.FindAsync(id);
        //    if (ticketProcessingVietN == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ProcessingTypeVietNid"] = new SelectList(_context.ProcessingTypeVietNs, "ProcessingTypeVietNid", "TypeCode", ticketProcessingVietN.ProcessingTypeVietNid);
        //    return View(ticketProcessingVietN);
        //}

        //// POST: TicketProcessingVietNs/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("TicketProcessingVietNid,ProcessingCode,TicketReference,ProcessingTypeVietNid,ProcessingAction,ActionDescription,RelatedTicketCode,PriorityLevel,Status,OverdueDays,EscalationLevel,IsAutoProcessed,ProcessedBy,ProcessedDate,ResolvedNote,CreatedDate,ModifiedDate")] TicketProcessingVietN ticketProcessingVietN)
        //{
        //    if (id != ticketProcessingVietN.TicketProcessingVietNid)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ticketProcessingVietN);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TicketProcessingVietNExists(ticketProcessingVietN.TicketProcessingVietNid))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ProcessingTypeVietNid"] = new SelectList(_context.ProcessingTypeVietNs, "ProcessingTypeVietNid", "TypeCode", ticketProcessingVietN.ProcessingTypeVietNid);
        //    return View(ticketProcessingVietN);
        //}

        //// GET: TicketProcessingVietNs/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticketProcessingVietN = await _context.TicketProcessingVietNs
        //        .Include(t => t.ProcessingTypeVietN)
        //        .FirstOrDefaultAsync(m => m.TicketProcessingVietNid == id);
        //    if (ticketProcessingVietN == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ticketProcessingVietN);
        //}

        // POST: TicketProcessingVietNs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
