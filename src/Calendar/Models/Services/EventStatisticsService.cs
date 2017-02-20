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


namespace Calendar.Models.Services
{
    public class EventStatisticsService : TeamProjectStat
    {
        private readonly ApplicationDbContext _context;
        public int SectionEventCount { get; private set; }
        public List<TeamProjectStat> TeamStats { get; private set; }

        public EventStatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void Initialize(DateTime FirstDateOfTheCalendar, DateTime LastDateOfTheCalendar)
        {
            
            SectionEventCount = 0;            

            var evt = from m in _context.Event.Where(m => !(m.EndDateTime <= FirstDateOfTheCalendar
                                                            || m.StartDateTime >= LastDateOfTheCalendar))
                      select m;

            var team = from m in _context.Team.OrderBy(m => m.Name)
                       select m;

            var teamproject = from m in _context.TeamProject.OrderBy(m => m.Team).OrderBy(n => n.Project)
                              select m;


            SectionEventCount = evt.Count();
            TeamStats = new List<TeamProjectStat>();            

            foreach (var t in team)
            {
                TeamProjectStat stat = new TeamProjectStat();
                
                var team_evt = evt.Where(m => m.AffectedTeams.Contains(t.Name));

                stat.Team = t.Name;
                stat.TeamEventCount = team_evt.Count();

                stat.Projects = new List<String>();
                stat.ProjectEventCounts = new List<int>();

                foreach (var tp in teamproject.Where(m => m.Team == t.Name))
                {
                    var tp_evt = team_evt.Where(m => m.AffectedProjects.Contains(tp.Project));
                    
                    stat.Projects.Add(tp.Project);
                    stat.ProjectEventCounts.Add(tp_evt.Count());
                }
                TeamStats.Add(stat);
            }
        }
    }
}
