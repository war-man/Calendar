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
    public class EventsController : Controller
    {
        const int DAYSINAWEEK = 7;
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Event.OrderByDescending(m => m.StartDateTime).ToListAsync());
        }


        // GET: Events/Edit/5
        public async Task<IActionResult> Calendar(int? year, int? month, string filterProject, string filterTeam)
        {
            DateTime now = System.DateTime.Now;
            DateTime ViewDate;

            if (year == null)
            {
                year = now.Year;
                month = now.Month;
            } else if (month == null)
            {
                month = now.Month;                
            }

            try
            {
                ViewDate = new DateTime((int)year, (int)month, 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }

            /* Prefer using ViewBag instead of VieData */
            ViewBag.ViewDate = ViewDate;

            DateTime FirstDateofTheMonth  = new DateTime(ViewDate.Year, ViewDate.Month, 1);
            DateTime FirstDateofNextMonth = new DateTime(ViewDate.Year, ViewDate.Month, 1).AddMonths(1);
            DateTime LastDateofTheMonth   = new DateTime(ViewDate.Year, ViewDate.Month, 1).AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            DateTime FirstDateOfTheCalendar = FirstDateofTheMonth.AddDays((int)FirstDateofTheMonth.DayOfWeek * -1);
            DateTime LastDateOfTheCalendar = LastDateofTheMonth.Date.AddDays(DAYSINAWEEK - 1 - (int)LastDateofTheMonth.DayOfWeek).AddHours(23).AddMinutes(59).AddSeconds(59);

            var e = from m in _context.Event.Where(m => !(m.EndDateTime <= FirstDateOfTheCalendar
                                                            || m.StartDateTime >= LastDateOfTheCalendar))
                         select m;

            if (!String.IsNullOrEmpty(filterProject))
            {
                ViewBag.FilterProject = filterProject.ToUpper();
                e = e.Where(m => m.AffectedProjects.Contains(filterProject.ToUpper()));                                             
            }

            if (!String.IsNullOrEmpty(filterTeam))
            {
                ViewBag.FilterTeam = filterTeam.ToUpper();
                e = e.Where(m => m.AffectedTeams.Contains(filterTeam.ToUpper()));
            }

            var @events = await e.OrderBy(m => m.StartDateTime).ToListAsync();
            if (@events == null)
            {
                return NotFound();
            }

            /* To maintain separation of concerns in mvc. */
            List<CalendarEvent> CalEvents = new List<CalendarEvent>();

            foreach (var item in @events)
            {
                CalendarEvent ce = new CalendarEvent(item);

                ce.Servers = item.AffectedHosts.Split(',').Select(p => p.Trim()).ToList();
                ce.Projects = item.AffectedProjects.Split(',').Select(p => p.Trim()).ToList();
                ce.Teams = item.AffectedTeams.Split(',').Select(p => p.Trim()).ToList();

                /* we need to trim the startdate and enddate */
                if (ce.e.StartDateTime < FirstDateOfTheCalendar)
                {
                    ce.e.StartDateTime = FirstDateOfTheCalendar;
                    ce.Continue = true;
                }

                if (ce.e.EndDateTime > LastDateOfTheCalendar)
                {
                    ce.e.EndDateTime = LastDateOfTheCalendar;
                }

                CalEvents.Add(ce);
            }

            return View(CalEvents);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AffectedHosts,AffectedProjects,Category,EndDateTime,Reference,Result,StartDateTime,Subject,TaskDescription,AffectedTeams,Severity")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AffectedHosts,AffectedProjects,Category,EndDateTime,Reference,Result,StartDateTime,Subject,TaskDescription,AffectedTeams,Severity")] Event @event)
        {
            if (id != @event.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.ID))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.ID == id);
        }
    }
}
