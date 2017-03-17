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
        public const string TIP_SUBJECT   = "The <i>Subject</i> should be as specific as possible and contains the information on the matters being dealt with.";
        public const string PHD_SUBJECT   = "It should be as specific as possible and contains the information on the matters being dealt with.";
        public const string TIP_SHORTDESC = "The <i>Short Description</i> will be displayed in Calendar view. It is recommended to make it short and precise.";
        public const string PHD_SHORTDESC = "It will be displayed in Calendar view. It is recommended to make it short and precise.";
        public const string TIP_STARTTIME = "Schedule Start Time";
        public const string PHD_STARTTIME = TIP_STARTTIME;
        public const string TIP_ENDTIME   = "Schedule End Time";
        public const string PHD_ENDTIME   = TIP_ENDTIME;
        public const string TIP_STATUS    = "Status of the Event";
        public const string PHD_STATUS    = TIP_STATUS;
        public const string TIP_REFERENCE = "TOSS's CR #, BSD Case ID and SCR # are good candidates of Reference #.";
        public const string PHD_REFERENCE = "e.g. TOSS's CR #, BSD Case ID or SCR #, etc.";

        public const string TIP_HOSTS    = "List of Hosts that will be affected.";
        public const string PHD_HOSTS    = TIP_HOSTS;
        public const string TIP_PROJECTS = "List of Projects that will be affected.";
        public const string PHD_PROJECTS = TIP_PROJECTS;
        public const string TIP_TEAMS    = "List of Project Teams that will be affected.";
        public const string PHD_TEAMS    = TIP_TEAMS;
        public const string TIP_CATEGORY = "<i>Category</i> can be OS Upgrade, Driver Upgrade, Security Patches, Hardware Upgrade, Hardware Replacement, etc.";
        public const string PHD_CATEGORY = "e.g. OS Upgrade, Driver Upgrade, Security Patches, Hardware Upgrade, Hardware Replacement, etc.";

        public const string TIP_IMPACTANALYSIS = "The details on what and how the system functions and services availability be affected by the change.";
        public const string PHD_IMPACTANALYSIS = TIP_IMPACTANALYSIS;
        public const string TIP_ACTIONPLAN = "The detail step-by-step actions to be executed during the change.";
        public const string PHD_ACTIONPLAN = TIP_ACTIONPLAN;
        public const string TIP_VERIFICATIONSTEPS = "The steps to verify and confirm the change has achieved the expected outcome.";
        public const string PHD_VERIFICATIONSTEPS = TIP_VERIFICATIONSTEPS;
        public const string TIP_FALLBACKPROC = "The procedure for aborting and recovering from an unsuccessful change. For change with Moderate or higher <i>Risk Level</i>, time should be allowed for carrying out the fallback procedure.";
        public const string PHD_FALLBACKPROC = "The procedure for aborting and recovering from an unsuccessful change. For change with Moderate or higher Risk Level, time should be allowed for carrying out the fallback procedure.";
        public const string TIP_RESULTS = "Describe the results of the change. Suggestion or improvement can also be put here.";
        public const string PHD_RESULTS = TIP_RESULTS;
        public const string TIP_LIKELIHOOD = "Please assess how likely the change fails to deliver the expected outcome. The <i>Failure Likelihood</i>, togehter with the <i>Failure Impact</i>, determines the <i>Risk Level</i>.";
        public const string PHD_LIKELIHOOD = "Please assess how likely the change fails to deliver the expected outcome.";
        public const string TIP_IMPACT     = "Please assess the impact to services if the change fails. The <i>Failure Impact</i>, togehter with the <i>Failure Likelihood</i>, determines the <i>Risk Level</i>.";
        public const string PHD_IMPACT     = "Please assess the impact to services if the change fails.";
        public const string TIP_RISKLEVEL  = "The <i>Risk Level</i> is determined by the <i>Failure Likelihood</i> and <i>Failure Impact</i>.";
        public const string PHD_RISKLEVEL = "The Risk Level is determined by the Failure Likelihood and Failure Impact.";

        public const string TIP_RISKANALYSIS = "Please review the risk assoicated with the change and provide actions or options to mitigate the risks to a minimium acceptable level.";
        public const string PHD_RISKANALYSIS = TIP_RISKANALYSIS;

        public const string ROLE_ADMIN = "Admins";

        public const string STATUS_CANCELLED = "CL";
        public const string STATUS_COMPLETED = "CMP";
        public const string STATUS_INCOMPLETE = "ICM";
        public const string STATUS_RFC = "RFC";
        public const string STATUS_SCHDCONFIRMED = "SC";
        public const string STATUS_TENTATIVE = "T";
    }
}
