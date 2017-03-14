using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class TeamProject
    {
        public int ID { get; set; }
        [StringLength(15, MinimumLength = 2), Required]
        public string Team { get; set; }
        [StringLength(15, MinimumLength = 2), Required]
        public string Project { get; set; }
    }
}
