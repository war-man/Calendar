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
                             AffectedTeams = "HA2,HA6,HA7",
                             AffectedHosts = "DC6/WCDCIIS08S",
                             TaskDescription = "Perform IP Change Drill Test ",
                             Reference = "CR16112804",
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
                             Result = "Successfully completed on schedule"
                         }
                    );
                    context.SaveChanges();
                }



                // Look for any Event.
                if (!context.Team.Any())
                {
                    context.Team.AddRange(
                         new Team
                         {
                             Name = "HA1",
                             Description = "Enterprise Resource Planning Project Team 1"
                         },
                         new Team
                         {
                             Name = "HA2",
                             Description = "Enterprise Resource Planning Project Team 2"
                         },
                         new Team
                         {
                             Name = "HA3",
                             Description = "Enterprise Resource Planning Project Team 3"
                         },
                         new Team
                         {
                             Name = "HA4",
                             Description = "Enterprise Resource Planning Project Team 4"
                         },
                         new Team
                         {
                             Name = "HA5",
                             Description = "Enterprise Resource Planning Project Team 5"
                         },
                         new Team
                         {
                             Name = "HA6",
                             Description = "Enterprise Resource Planning Project Team 6"
                         },

                         new Team
                         {
                             Name = "HA7",
                             Description = "Enterprise Resource Planning Project Team 7"
                         }
                         );
                    context.SaveChanges();
                }


                // Look for any Event.
                if (!context.Project.Any())
                {
                    context.Project.AddRange(
                        new Project
                        {
                            Name ="ERP",
                            Description="ERP",
                            Administrator = "HA4"
                        },
                        new Project
                        {
                            Name = "HYP",
                            Description = "Hyperion Planning",
                            Administrator = "HA4"
                        },
                        new Project
                        {
                            Name = "SFMS",
                            Description = "SamFund",
                            Administrator = "HA4"
                        },
                         new Project
                        {
                            Name = "BA",
                            Description = "Business Intelligence",
                            Administrator = "HA4"
                         }
                    );
                    context.SaveChanges();

                }
            }
        }
    }
}
