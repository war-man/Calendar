using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Helpers
{
    public class TeamProjectStat
    {
        public String Team { get; set; }
        public int TeamEventCount { get; set; }
        public List<String> Projects { get; set; }
        public List<int> ProjectEventCounts { get; set; }
    }
}
