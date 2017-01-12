using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Models;

namespace Calendar.Helpers
{
    public class CalendarEvent
    {
        public Event e { get; set; }
        public DateTime OriginalStart { get; set; }
        public DateTime OriginalEnd { get; set; }
        public bool Continue { get; set; }
        public bool MarkedInCalendar { get; set; }
        public int ColorIdx { get; set; }
        public List<String> Projects { get; set; }
        public List<String> Teams { get; set; }
        public List<String> Servers { get; set; }
        // Constructor
        public CalendarEvent(Event e)
        {
            this.e = e;
            this.OriginalStart = e.StartDateTime;
            this.OriginalEnd = e.EndDateTime;
            this.Continue = false;
            this.MarkedInCalendar = false;
        }
    }
}
