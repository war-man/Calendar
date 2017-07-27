using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class Attachment
    {
        public int ID { get; set; }
        [Required]
        public int EventID { get; set; }
        [StringLength(255), Required]
        public string FileName { get; set; }
        [StringLength(500), Required]
        public string FilePath { get; set; }

        [Display(Name = "Creation Date"), DisplayFormat(DataFormatString = "{0:MMM d, yyyy h:mm tt}", ApplyFormatInEditMode = false), DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Created By"), StringLength(50)]
        public string CreatedBy { get; set; }
        [Display(Name = "Created By"), StringLength(50)]
        public string CreatedByDisplayName { get; set; }
        [Display(Name = "Last Updated"), DisplayFormat(DataFormatString = "{0:MMM d, yyyy h:mm tt}", ApplyFormatInEditMode = false), DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }
        [Display(Name = "Updated By"), StringLength(50)]
        public string UpdatedBy { get; set; }
        [Display(Name = "Updated By"), StringLength(50)]
        public string UpdatedByDisplayName { get; set; }
    }
}
