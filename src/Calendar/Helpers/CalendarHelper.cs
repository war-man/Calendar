using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Models;

namespace Calendar.Helpers
{

    public class TeamProjectStat
    {
        public String Team { get; set; }
        public int TeamEventCount { get; set; }
        public List<String> Projects { get; set; }
        public List<int> ProjectEventCounts { get; set; }
    }


    /* CalendarEvent Class provided the data object used in the Calendar View */    
    /* Moved to CalendarEventViewModel !!! */

    /* Provide a single place to define all the Constants */
    public static class Constants
    {
        public const string TIP_SHORTDESC  = "The <i>Short Description</i> will be displayed in Calendar view. It is recommended to make it short and precise.";
        public const string TIP_LIKELIHOOD = "Please assess how likely this maintenance fails to deliver the expected outcome. The <i>Likelihood</i>, togehter with the <i>Impact</i>, determines the <i>Risk Level</i>.";
        public const string TIP_IMPACT     = "Please assess the impact to services if this maintenance fails. The <i>Impact</i>, togehter with the <i>Likelihood</i>, determines the <i>Risk Level</i>.";
        public const string TIP_RISKLEVEL  = "The <i>Risk Level</i> is determined by the <i>Likelihood</i> and <i>Impact</i>.";

        public const string ROLE_ADMIN = "Admins";
    }
}
