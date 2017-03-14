using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Calendar.Data;
using System;
using System.Linq;

namespace Calendar.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any Event.
                if (!context.Event.Any())
                {
                    context.Event.AddRange(
                         new Event
                         {
                             StartDateTime = DateTime.Parse("2016-12-08 18:00"),
                             EndDateTime = DateTime.Parse("2016-12-08 20:00"),
                             Subject = "Schedule Confirmed (Service Impact) (Windows) DC6 WCDCIIS08S - Perform IP Change Drill Test in the OS instance(s) on 8 Dec 2016 (CR16112804)",
                             Category = "Server",
                             AffectedProjects = "ERP",
                             AffectedTeams = "HA2, HA6, HA7",
                             AffectedHosts = "DC6/WCDCIIS08S",
                             TaskDescription = "Perform IP Change Drill Test ",
                             Reference = "CR16112804",
                             Environment = "DEV",
                             Likelihood = "10",
                             Impact = "10",
                             RiskLevel = "10",
                             Result = "Successfully completed on schedule"
                         },
                         new Event
                         {
                             StartDateTime = DateTime.Parse("2016-12-11 01:00"),
                             EndDateTime = DateTime.Parse("2016-12-11 04:00"),
                             Subject = "Schedule Confirmed (Service Impact) (Unix) DC7 erp77014 - Voltage Regulator Module replacement on 11 Dec 2016 (PR1855565)",
                             Category = "Server",
                             AffectedProjects = "HYP",
                             AffectedTeams = "HA2",
                             AffectedHosts = "DC7/erp77014",
                             TaskDescription = "Voltage Regulator Module replacement ",
                             Reference = "PR1855565",
                             Environment = "DEV",
                             Likelihood = "10",
                             Impact = "10",
                             RiskLevel = "10",
                             Result = "Successfully completed on schedule"
                         }
                    );
                    context.SaveChanges();
                }

                // Look for any Event.
                if (!context.Team.Any())
                {
                    context.Team.AddRange(
                         new Team { Name = "HA1", Description = "Enterprise Resource Planning Project Team 1" },
                         new Team { Name = "HA2", Description = "Enterprise Resource Planning Project Team 2" },
                         new Team { Name = "HA3", Description = "Enterprise Resource Planning Project Team 3" },
                         new Team { Name = "HA4", Description = "Enterprise Resource Planning Project Team 4" },
                         new Team { Name = "HA5", Description = "Enterprise Resource Planning Project Team 5" },
                         new Team { Name = "HA6", Description = "Enterprise Resource Planning Project Team 6" },
                         new Team { Name = "HA7", Description = "Enterprise Resource Planning Project Team 7" },
                         new Team { Name = "HA8", Description = "Enterprise Resource Planning Project Team 8" },
                         new Team { Name = "HA9", Description = "Enterprise Resource Planning Project Team 9" },
                         new Team { Name = "HAS", Description = "HAS" }
                         );
                    context.SaveChanges();
                }


                // Look for any Event.
                if (!context.Project.Any())
                {
                    context.Project.AddRange(
new Project { Name = "NTSS", Description = "NEATS Transport Supporting System", Administrator = "HA1" },
new Project { Name = "EWS", Description = "Electronic Waiving System", Administrator = "HA1" },
new Project { Name = "DCMS", Description = "Dietetics & Catering Management System", Administrator = "HA1" },
new Project { Name = "OCSSS", Description = "Online Checking System for Subsidized Public Healthcare Services", Administrator = "HA1" },
new Project { Name = "ITBudie", Description = "IT Budget Intranet Explorer", Administrator = "HA4" },
new Project { Name = "Hyperion", Description = "ERP Budget Planning (Hyperion)", Administrator = "HA4" },
new Project { Name = "CRS", Description = "Casemix Reporting System", Administrator = "HA4" },
new Project { Name = "CS", Description = "Costing System", Administrator = "HA4" },
new Project { Name = "FMRS", Description = "Financial Management Reporting System", Administrator = "HA4" },
new Project { Name = "SFMS", Description = "Samaritan Fund Management System", Administrator = "HA4" },
new Project { Name = "SITs", Description = "Surgical Instrument Tracking System", Administrator = "HA4" },
new Project { Name = "PTNT", Description = "Product Tracking & Tracing", Administrator = "HA4" },
new Project { Name = "BSSD", Description = "BSSD Home Page", Administrator = "HA4" },
new Project { Name = "CMBC", Description = "Contract Management Bulk Contracts", Administrator = "HA4" },
new Project { Name = "PVIS", Description = "Patient Valuables Information System", Administrator = "HA4" },
new Project { Name = "COPPE", Description = "Capital Project Planning & Monitoring System", Administrator = "HA4" },
new Project { Name = "SCR", Description = "System Change Request", Administrator = "HA4" },
new Project { Name = "ERP", Description = "Enterprise Resource Planning", Administrator = "HA4" },
new Project { Name = "BSD", Description = "Business Support Desk", Administrator = "HA4" },
new Project { Name = "CUID", Description = "CUID.Home", Administrator = "HA4" },
new Project { Name = "PBRC", Description = "Patient Billing and Revenue Collection System", Administrator = "HA5" },
new Project { Name = "SRS", Description = "Staff Rostering System", Administrator = "HA4" },
new Project { Name = "MCS", Description = "Medical Clerkship System", Administrator = "HA4" },
new Project { Name = "SESAS", Description = "Staff Early Sickness Alert System", Administrator = "HA4" },
new Project { Name = "LEAPS", Description = "Leave Enquiry and ePayslip System", Administrator = "HA4" },
new Project { Name = "HLISS", Description = "Home Loan Interest Subsidy Scheme", Administrator = "HA4" },
new Project { Name = "eRecruitment", Description = "eRecruitment System", Administrator = "HA4" },
new Project { Name = "RMS", Description = "Resource Management System", Administrator = "HA4" },
new Project { Name = "DCRm", Description = "Doctor Rostering Management System", Administrator = "HA4" },
new Project { Name = "CSIS", Description = "Contract Staff Information System", Administrator = "HA4" },
new Project { Name = "MSS", Description = "HR App", Administrator = "HA4" },
new Project { Name = "ERP-UAM", Description = "ERP User Account Management", Administrator = "HA4" },
new Project { Name = "BI", Description = "Business Intelligence", Administrator = "HA4" }
                    );
                    context.SaveChanges();
                }

                // Look for any TeamProject Relationships.
                if (!context.TeamProject.Any())
                {
                    context.TeamProject.AddRange(
new TeamProject { Team = "HA1", Project = "NTSS" },
new TeamProject { Team = "HA1", Project = "EWS" },
new TeamProject { Team = "HA1", Project = "DCMS" },
new TeamProject { Team = "HA1", Project = "OCSSS" },
new TeamProject { Team = "HA2", Project = "ITBudie" },
new TeamProject { Team = "HA2", Project = "Hyperion" },
new TeamProject { Team = "HA2", Project = "CRS" },
new TeamProject { Team = "HA2", Project = "CS" },
new TeamProject { Team = "HA2", Project = "FMRS" },
new TeamProject { Team = "HA2", Project = "SFMS" },
new TeamProject { Team = "HA3", Project = "SITs" },
new TeamProject { Team = "HA3", Project = "PTNT" },
new TeamProject { Team = "HA3", Project = "BSSD" },
new TeamProject { Team = "HA3", Project = "CMBC" },
new TeamProject { Team = "HA3", Project = "PVIS" },
new TeamProject { Team = "HA3", Project = "COPPE" },
new TeamProject { Team = "HA4", Project = "SCR" },
new TeamProject { Team = "HA4", Project = "BSD" },
new TeamProject { Team = "HA4", Project = "CUID" },
new TeamProject { Team = "HA5", Project = "PBRC" },
new TeamProject { Team = "HA7", Project = "SRS" },
new TeamProject { Team = "HA7", Project = "MCS" },
new TeamProject { Team = "HA7", Project = "SESAS" },
new TeamProject { Team = "HA7", Project = "LEAPS" },
new TeamProject { Team = "HA7", Project = "HLISS" },
new TeamProject { Team = "HA7", Project = "eRecruitment" },
new TeamProject { Team = "HA7", Project = "RMS" },
new TeamProject { Team = "HA7", Project = "DCRm" },
new TeamProject { Team = "HA7", Project = "CSIS" },
new TeamProject { Team = "HA7", Project = "MSS" },
new TeamProject { Team = "HA9", Project = "ERP-UAM" },
new TeamProject { Team = "HAS", Project = "BI" },
new TeamProject { Team = "HA2", Project = "ERP" },
new TeamProject { Team = "HA6", Project = "ERP" },
new TeamProject { Team = "HA7", Project = "ERP" },
new TeamProject { Team = "HA8", Project = "ERP" },
new TeamProject { Team = "HA9", Project = "ERP" }
                    );
                    context.SaveChanges();

                }
            }
        }
    }
}
