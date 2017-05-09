using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class Event : IValidatableObject
    {
        public int ID { get; set; }
        [Display(Name = "Start Time"), DisplayFormat(DataFormatString = "{0:MMM d, yyyy h:mm tt}", ApplyFormatInEditMode = true), DataType(DataType.DateTime), Required]
        public DateTime StartDateTime { get; set; }
        [Display(Name = "End Time"), DisplayFormat(DataFormatString = "{0:MMM d, yyyy h:mm tt}", ApplyFormatInEditMode = true), DataType(DataType.DateTime), Required]
        public DateTime EndDateTime { get; set; }
        [StringLength(500, MinimumLength = 3), Required]
        public string Subject { get; set; }
        [Display(Name = "Short Description"), StringLength(500, MinimumLength = 3), Required]
        public string TaskDescription { get; set; }
        [StringLength(50, MinimumLength = 3), Required]
        public string Category { get; set; }
        [Display(Name = "Hosts"), StringLength(150, MinimumLength = 3), Required]
        public string AffectedHosts { get; set; }
        [Display(Name = "Projects"), StringLength(200, MinimumLength = 2), Required]
        public string AffectedProjects { get; set; }
        [Display(Name = "Risk Level"), StringLength(10), Required]
        public string RiskLevel { get; set; }
        [Display(Name = "Teams"), StringLength(150, MinimumLength = 2), Required]
        public string AffectedTeams { get; set; }
        [Display(Name = "Reference"), StringLength(50)]
        public string Reference { get; set; }
        [Display(Name = "Results"), StringLength(100, MinimumLength = 3)]
        public string Result { get; set; }

        [Display(Name = "Environment"), StringLength(10), Required]
        public string Environment { get; set; }
        [Display(Name = "Action By"), StringLength(100)]
        public string ActionBy { get; set; }
        [Display(Name = "Health Check By"), StringLength(100)]
        public string HealthCheckBy { get; set; }
        [Display(Name = "Failure Likelihood"), StringLength(10), Required]
        public string Likelihood { get; set; }
        [Display(Name = "Failure Impact"), StringLength(10), Required]
        public string Impact { get; set; }

        [Display(Name = "Status"), StringLength(5)]
        public string EventStatus { get; set; }        
        [Display(Name = "Service Impact"), StringLength(1000), DataType(DataType.MultilineText)]
        public string ImpactAnalysis { get; set; }
        [Display(Name = "Action Plan"), StringLength(1000)]
        public string MaintProcedure { get; set; }
        [Display(Name = "Verification Steps"), StringLength(1000)]
        public string VerificationStep { get; set; }
        [Display(Name = "Fallback Procedure"), StringLength(1000)]
        public string FallbackProcedure { get; set; }

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

        [Display(Name = "Risk Analysis"), StringLength(1000), DataType(DataType.MultilineText)]
        public string RiskAnalysis { get; set; }

        /* The Severity is to be obsolete. Once commented, EF will drop the column when execute "dotnet ef database update" */
        //[Display(Name = "Severity")]
        //public int Severity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDateTime > EndDateTime)
                yield return new ValidationResult("Start Time cannot be later than End Time.", new[] { "StartDateTime", "EndDateTime" });
        }
        /* navigation properties */
        public ICollection<Acknowledgement> Acknowledgements { get; set; }
    }
}
