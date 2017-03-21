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
        
        /* It will be good if we can pass the Model in the Calendar.cshtml so that we don't need to run the query again. */
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

            /* let's count the number of all ***UNIQUE*** maintenance events for all levels */
            
//            foreach (var t in team)
//            {
//                TeamProjectStat stat = new TeamProjectStat();
//
//                stat.Team = t.Name;
//                stat.TeamEventCount = 0;
//                stat.Projects = new List<String>();
//                stat.ProjectEventCounts = new List<int>();
//
//                /* make use of dictionary object to get the unique event ids for each team*/
//                Dictionary<int, int> teamevent_ids = new Dictionary<int, int>();
//
//                foreach (var tp in teamproject.Where(m => m.Team == t.Name))
//                {
//                    var tp_evt = evt.Where(m => ("," + m.AffectedProjects + ",").Contains("," + tp.Project + ",")
//                                             && ("," + m.AffectedTeams + ",").Contains("," + t.Name + ","));

//                    stat.Projects.Add(tp.Project);
//                    stat.ProjectEventCounts.Add(tp_evt.Count());
//                    //stat.TeamEventCount = stat.TeamEventCount + tp_evt.Count();

//                    foreach (var e in tp_evt)
//                    {
//                        if (!teamevent_ids.ContainsKey(e.ID))
//                        {
//                            teamevent_ids.Add(e.ID, e.ID);
//                        }
//                    }
//                }
//                stat.TeamEventCount = teamevent_ids.Count();
//                TeamStats.Add(stat);
//            }
            

            
            foreach (var t in team)
            {
                TeamProjectStat stat = new TeamProjectStat();
                
                var team_evt = evt.Where(m => (","+m.AffectedTeams+",").Contains(","+t.Name+","));

                stat.Team = t.Name;
                stat.TeamEventCount = team_evt.Count();

                stat.Projects = new List<String>();
                stat.ProjectEventCounts = new List<int>();

                foreach (var tp in teamproject.Where(m => m.Team == t.Name))
                {
                    var tp_evt = team_evt.Where(m => (","+m.AffectedProjects+",").Contains(","+tp.Project+","));
                    
                    stat.Projects.Add(tp.Project);
                    stat.ProjectEventCounts.Add(tp_evt.Count());
                }
                TeamStats.Add(stat);
            }
        }
    }
}
