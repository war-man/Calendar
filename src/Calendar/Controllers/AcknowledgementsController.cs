using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;
using Calendar.Helpers;

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
        public async Task<IActionResult> Index(int? eventid)
        {
            if (eventid == null)
                return View(await _context.Acknowledgement.ToListAsync());
            else
            {
                return View(await _context.Acknowledgement.Where(m => m.EventID == eventid).ToListAsync());
            }
        }

        // GET: Acknowledgements
        public async Task<IActionResult> IndexPartial(int eventid)
        {
            return PartialView("AckPartial", await _context.Acknowledgement.Where(m => m.EventID == eventid).ToListAsync());
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
        public async Task<IActionResult> Create([Bind("ID,AckMessage,CreatedBy,CreatedByDisplayName,CreatedDate,EventID,Team,UpdatedBy,UpdatedByDisplayName,UpdatedDate")] Acknowledgement acknowledgement, string redir, string ajax)
        {
            if (!User.Identity.IsAuthenticated)
                return NotFound();

            if (ModelState.IsValid)
            {
                /* Audit Fields */
                var username = "anonymous";
                var u = User.Claims.Where(m => m.Type == "username");
                if (u.Count() == 1) { username = u.First().Value; }
                var displayname = "anonymous";
                var d = User.Claims.Where(m => m.Type == "displayName");
                if (d.Count() == 1) { displayname = d.First().Value; }
                acknowledgement.CreatedDate = DateTime.Now;
                acknowledgement.CreatedBy = username;
                acknowledgement.CreatedByDisplayName = displayname;
                acknowledgement.UpdatedDate = acknowledgement.CreatedDate;
                acknowledgement.UpdatedBy = username;
                acknowledgement.UpdatedByDisplayName = displayname;


                _context.Add(acknowledgement);
                await _context.SaveChangesAsync();


                if (ajax == "true")
                    return new EmptyResult();
                else if (redir == "")
                    return RedirectToAction("Index");
                else
                {
                    RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Events", new { @id = acknowledgement.EventID, @redir = redir });
                    return redirectResult;
                }

            }
            return View(acknowledgement);
        }

        // GET: Acknowledgements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                return NotFound();

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
            if (!User.Identity.IsAuthenticated)
                    return NotFound();

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
            if (!User.Identity.IsAuthenticated)
                return NotFound();

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
        public async Task<IActionResult> DeleteConfirmed(int id, string ajax)
        {
            if (!User.Identity.IsAuthenticated)
                return NotFound();

            var acknowledgement = await _context.Acknowledgement.SingleOrDefaultAsync(m => m.ID == id);
            _context.Acknowledgement.Remove(acknowledgement);
            await _context.SaveChangesAsync();
            if (ajax == "true")
                return new EmptyResult();
            else
                return RedirectToAction("Index");
        }

        private bool AcknowledgementExists(int id)
        {
            return _context.Acknowledgement.Any(e => e.ID == id);
        }
    }
}
