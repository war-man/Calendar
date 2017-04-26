using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class Team
    {
        public int ID { get; set; }
        [StringLength(15, MinimumLength = 2), Required]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 3), Required]
        public string Description { get; set; }
        [Display(Name = "Calendar Style"), StringLength(15, MinimumLength = 6)]
        public string CalendarStyle { get; set; }
        [Display(Name = "Domain Group")]
        public string DomainGroup { get; set; }
    }
}
