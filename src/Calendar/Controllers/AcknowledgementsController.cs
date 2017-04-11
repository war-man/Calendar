using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;

namespace Calendar.Controllers
{
    public class AcknowledgementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AcknowledgementsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Acknowledgements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Acknowledgement.ToListAsync());
        }

        // GET: Acknowledgements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acknowledgement = await _context.Acknowledgement.SingleOrDefaultAsync(m => m.ID == id);
            if (acknowledgement == null)
            {
                return NotFound();
            }

            return View(acknowledgement);
        }

        // GET: Acknowledgements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acknowledgements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AckMessage,CreatedBy,CreatedByDisplayName,CreatedDate,EventID,Team,UpdatedBy,UpdatedByDisplayName,UpdatedDate")] Acknowledgement acknowledgement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acknowledgement);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(acknowledgement);
        }

        // GET: Acknowledgements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acknowledgement = await _context.Acknowledgement.SingleOrDefaultAsync(m => m.ID == id);
            if (acknowledgement == null)
            {
                return NotFound();
            }
            return View(acknowledgement);
        }

        // POST: Acknowledgements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AckMessage,CreatedBy,CreatedByDisplayName,CreatedDate,EventID,Team,UpdatedBy,UpdatedByDisplayName,UpdatedDate")] Acknowledgement acknowledgement)
        {
            if (id != acknowledgement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acknowledgement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcknowledgementExists(acknowledgement.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(acknowledgement);
        }

        // GET: Acknowledgements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acknowledgement = await _context.Acknowledgement.SingleOrDefaultAsync(m => m.ID == id);
            if (acknowledgement == null)
            {
                return NotFound();
            }

            return View(acknowledgement);
        }

        // POST: Acknowledgements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acknowledgement = await _context.Acknowledgement.SingleOrDefaultAsync(m => m.ID == id);
            _context.Acknowledgement.Remove(acknowledgement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AcknowledgementExists(int id)
        {
            return _context.Acknowledgement.Any(e => e.ID == id);
        }
    }
}
