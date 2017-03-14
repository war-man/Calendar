using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;
using Calendar.Models.CalendarViewModels;
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
        //startdate_desc, startdate_asce, creation_desc, creation_asce
        public async Task<IActionResult> Index(string sort)
        {

            ViewBag.SortParm = String.IsNullOrEmpty(sort) ? "" : sort;
            
            var events = from e in _context.Event
                           select e;

            switch (sort)
            {
                case "cd_a":
                    events = events.OrderBy(e => e.CreatedDate);
                    break;
                case "cd_d":
                    events = events.OrderByDescending(e => e.CreatedDate);
                    break;
                case "sd_a":
                    events = events.OrderBy(e => e.StartDateTime);
                    break;
                default:   // sd_d
                    events = events.OrderByDescending(e => e.StartDateTime);
                    break;
            }

            return View(await events.AsNoTracking().ToListAsync());
            //return View(await _context.Event.OrderByDescending(m => m.StartDateTime).ToListAsync());
        }


        // GET: Events/Calendar
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
                /* filter will be done in View */
                //e = e.Where(m => m.AffectedProjects.Contains(filterProject.ToUpper()));                                             
            }

            if (!String.IsNullOrEmpty(filterTeam))
            {
                ViewBag.FilterTeam = filterTeam.ToUpper();
                /* filter will be done in View */
                //e = e.Where(m => m.AffectedTeams.Contains(filterTeam.ToUpper()));
            }

            var @events = await e.OrderBy(m => m.StartDateTime).ToListAsync();
            if (@events == null)
            {
                return NotFound();
            }

            /* To maintain separation of concerns in mvc. */
            List<CalendarEventViewModel> CalEvents = new List<CalendarEventViewModel>();

            foreach (var item in @events)
            {
                CalendarEventViewModel ce = new CalendarEventViewModel(item);

                ce.Servers = item.AffectedHosts.Split(',').Select(p => p.Trim().ToUpper()).ToList();
                ce.Projects = item.AffectedProjects.Split(',').Select(p => p.Trim().ToUpper()).ToList();
                ce.Teams = item.AffectedTeams.Split(',').Select(p => p.Trim().ToUpper()).ToList();

                /* we need to trim the startdate and enddate */
                if (ce.Event.StartDateTime < FirstDateOfTheCalendar)
                {
                    ce.Event.StartDateTime = FirstDateOfTheCalendar;
                    ce.Continue = true;
                }

                if (ce.Event.EndDateTime > LastDateOfTheCalendar)
                {
                    ce.Event.EndDateTime = LastDateOfTheCalendar;
                }

                CalEvents.Add(ce);
            }

            return View(CalEvents);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id, string redir = null)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);

            ViewBag.Redir = redir;

            if (@event == null)
            {
                return NotFound();
            }

            CalendarEventViewModel CalendarEvent = new CalendarEventViewModel(@event);

            return View(CalendarEvent);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            if (User.IsInRole(Constants.ROLE_ADMIN))
                return View();
            else
                return NotFound();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AffectedHosts,AffectedProjects,Category,EndDateTime,Reference,Result,StartDateTime,Subject,TaskDescription,AffectedTeams,RiskLevel,Environment,ActionBy,HealthCheckBy,Likelihood,Impact,ImpactAnalysis,MaintProcedure,VerificationStep,FallbackProcedure,EventStatus,RiskAnalysis")] Event @event)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
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
                @event.CreatedDate = DateTime.Now;
                @event.CreatedBy = username;
                @event.CreatedByDisplayName = displayname;
                @event.UpdatedDate = @event.CreatedDate;
                @event.UpdatedBy = username;
                @event.UpdatedByDisplayName = displayname;

                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        /*
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
        */

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id, string redir)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewBag.Redir = redir;
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string redir, [Bind("ID,AffectedHosts,AffectedProjects,Category,EndDateTime,Reference,Result,StartDateTime,Subject,TaskDescription,AffectedTeams,RiskLevel,Environment,ActionBy,HealthCheckBy,Likelihood,Impact,ImpactAnalysis,MaintProcedure,VerificationStep,FallbackProcedure,EventStatus,CreatedDate,CreatedBy,CreatedByDisplayName,RiskAnalysis")] Event @event)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id != @event.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /* Audit Fields */
                    var username = "anonymous";
                    var u = User.Claims.Where(m => m.Type == "username");
                    if (u.Count() == 1) { username = u.First().Value; }
                    var displayname = "anonymous";
                    var d = User.Claims.Where(m => m.Type == "displayName");
                    if (d.Count() == 1) { displayname = d.First().Value; }
                    @event.UpdatedDate = DateTime.Now;
                    @event.UpdatedBy = username;
                    @event.UpdatedByDisplayName = displayname;
                    //replace all the spaces in Hosts and Projects
                    //@event.AffectedHosts = @event.AffectedHosts.Replace(" ", "");
                    //@event.AffectedHosts = @event.AffectedProjects.Replace(" ", "");

                    //The IsModified doesn't work!
                    //_context.Entry(@event).Property("CreatedBy").IsModified = false;
                    //_context.Entry(@event).Property("CreatedByDisplayName").IsModified = false;
                    //_context.Entry(@event).Property("CreatedDate").IsModified = false;

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
                //string sredir = redir.Replace("%3F", "?").Replace("%3D","=").Replace("%26","&");
                string sredir;
                string pYear = "", pMonth = "";

                if (redir != null && redir.StartsWith("Calendar"))
                {
                    sredir = Uri.UnescapeDataString(redir);
                    int qPos = sredir.IndexOf('?');
                    if ( qPos >= 0)
                    {
                        sredir = sredir.Substring(qPos+1, sredir.Length - (qPos+1));
                    }
                    string[] sparam = sredir.Split('&');

                    foreach (var p in sparam)
                    {
                        string[] svalue = p.Split('=');

                        if (svalue[0].ToLower() == "year")
                            pYear = svalue[1];
                        if (svalue[0].ToLower() == "month")
                            pMonth = svalue[1];
                    }

                    RedirectToActionResult redirectResult = new RedirectToActionResult("Calendar", "Events", new { @year = pYear, @month = pMonth });
                    //return RedirectToAction(Uri.UnescapeDataString(redir));
                    //return RedirectToAction(sredir);                
                    return redirectResult;
                }
                else
                    return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

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
