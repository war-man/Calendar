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
    public class TeamProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamProjectsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TeamProjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamProject.ToListAsync());
        }

        // GET: TeamProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var teamProject = await _context.TeamProject.SingleOrDefaultAsync(m => m.ID == id);
            if (teamProject == null)
            {
                return NotFound();
            }

            return View(teamProject);
        }

        // GET: TeamProjects/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            return View();
        }

        // POST: TeamProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Project,Team")] TeamProject teamProject)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Add(teamProject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(teamProject);
        }

        // GET: TeamProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var teamProject = await _context.TeamProject.SingleOrDefaultAsync(m => m.ID == id);
            if (teamProject == null)
            {
                return NotFound();
            }
            return View(teamProject);
        }

        // POST: TeamProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Project,Team")] TeamProject teamProject)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id != teamProject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamProjectExists(teamProject.ID))
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
            return View(teamProject);
        }

        // GET: TeamProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var teamProject = await _context.TeamProject.SingleOrDefaultAsync(m => m.ID == id);
            if (teamProject == null)
            {
                return NotFound();
            }

            return View(teamProject);
        }

        // POST: TeamProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            var teamProject = await _context.TeamProject.SingleOrDefaultAsync(m => m.ID == id);
            _context.TeamProject.Remove(teamProject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamProjectExists(int id)
        {
            return _context.TeamProject.Any(e => e.ID == id);
        }
    }
}
