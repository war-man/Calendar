using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class Event
    {
        public int ID { get; set; }
        [Display(Name = "Start Time"), DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm}"), DataType(DataType.DateTime), Required]
        public DateTime StartDateTime { get; set; }
        [Display(Name = "End Time"), DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm}"), DataType(DataType.DateTime), Required]
        public DateTime EndDateTime { get; set; }
        [StringLength(500, MinimumLength = 3)]
        public string Subject { get; set; }
        [Display(Name = "Short Description"), StringLength(500, MinimumLength = 3)]
        public string TaskDescription { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Category { get; set; }
        [Display(Name = "Hosts"), StringLength(150, MinimumLength = 3)]
        public string AffectedHosts { get; set; }
        [Display(Name = "Projects"), StringLength(150, MinimumLength = 2)]
        public string AffectedProjects { get; set; }
        [Display(Name = "Severity")]
        public int Severity { get; set; }
        [Display(Name = "Teams"), StringLength(150, MinimumLength = 2)]
        public string AffectedTeams { get; set; }
        [Display(Name = "Reference"), StringLength(50)]
        public string Reference { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Result { get; set; }        
    }
}
