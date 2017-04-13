using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Calendar.Models.Services;

namespace Calendar.Models.CalendarViewModels
{
    /* CalendarEvent Class provided the data object used in the Calendar View */
    public class CalendarEventViewModel
    {
        public Event Event { get; set; }
        public DateTime OriginalStart { get; set; }
        public DateTime OriginalEnd { get; set; }
        public bool Continue { get; set; }
        public bool MarkedInCalendar { get; set; }
        public int ColorIdx { get; set; }
        public List<String> Projects { get; set; }
        public List<String> Teams { get; set; }
        public List<String> Servers { get; set; }
        public string EventStatus { get; set; }
        public string RiskLevel { get; set; }
        public int PrevEventID { get; set; }
        public int NextEventID { get; set; }
        public string DisplayAffectedHosts { get; set; }
        public string DisplayAffectedTeams { get; set; }
        public string DisplayAffectedProjects { get; set; }
        
        /* List of acknowledgements */
        public List<Acknowledgement> Acks { get; set; }

        // Constructor
        public CalendarEventViewModel(Event e) : this(e, null) { }
        public CalendarEventViewModel(Event e, List<Acknowledgement> acks)
        {
            this.Event = e;
            this.OriginalStart = e.StartDateTime;
            this.OriginalEnd = e.EndDateTime;
            this.Continue = false;
            this.MarkedInCalendar = false;

            if (acks != null)
                this.Acks = acks;

            DisplayAffectedHosts = e.AffectedHosts.Replace(",", ", ");
            DisplayAffectedTeams = e.AffectedTeams.Replace(",", ", ");
            DisplayAffectedProjects = e.AffectedProjects.Replace(",", ", ");

            // Lookup those Names by Values, such as RiskLevel, EventStatus, etc.
            StaticListOfValuesService LOVs = new StaticListOfValuesService();


            var es = LOVs.ListEventStatus().Where(m => m.Value == e.EventStatus);
            if (es.Count() == 1) { this.EventStatus = es.First().Name; } else { this.EventStatus = e.EventStatus; }
            var rl = LOVs.ListRiskLevels().Where(m => m.Value == e.RiskLevel);
            if (rl.Count() == 1) { this.RiskLevel = rl.First().Name; } else { this.RiskLevel = e.RiskLevel; }
        }
    }

}

