using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/* All the static List of Values will be provied by this Services */
namespace Calendar.Models.Services
{
    public class LOV
    {
        public string Function { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public LOV (string pFunction, string pName, string pValue)
        {
            Function = pFunction;
            Name = pName;
            Value = pValue;      
        }
    }

    public class StaticListOfValuesService
    {
        const string RISK_VERYLOW  = "1";
        const string RISK_LOW      = "2";
        const string RISK_MODERATE = "3";
        const string RISK_HIGH     = "4";
        const string RISK_CRITICAL = "5";

        public List<LOV> ListSeverities()
        {
            return new List<LOV>()
            {
                new LOV("severity", "High", "1"),
                new LOV("severity", "Medium","2"),
                new LOV("severity", "Low","3")
            };
        }

        public List<string> ListColors()
        {
            return new List<string>() { "Blue", "Green", "Red", "Yellow" };
        }

        public List<LOV> ListEnvironments()
        {
            return new List<LOV>()
            {
                new LOV("Environment", "PROD", "PROD"),
            };
        }

        public List<LOV> ListLikelihoods()
        {
            return new List<LOV>()
            {
                new LOV("Likelihood", "Rare", "1"),
                new LOV("Likelihood", "Unlikely","2"),
                new LOV("Likelihood", "Possible","3"),
                new LOV("Likelihood", "Likely","4"),
                new LOV("Likelihood", "Almost certain","5")
            };
        }

        public List<LOV> ListImpacts()
        {
            return new List<LOV>()
            {
                new LOV("Impact", "Insignificant", "1"),
                new LOV("Impact", "Minor","2"),
                new LOV("Impact", "Moderate","3"),
                new LOV("Impact", "Major","4"),
                new LOV("Impact", "Catastrophic","5")
            };
        }

        public List<LOV> ListRiskLevels()
        {
            return new List<LOV>()
            {
                new LOV("RiskLevel", "Very low", "1"),
                new LOV("RiskLevel", "Low", "2"),
                new LOV("RiskLevel", "Moderate", "3"),
                new LOV("RiskLevel", "High", "4"),
                new LOV("RiskLevel", "Critical", "5")
            };
        }

        public string[,] RiskLevelMatrix = 
                new string[5,5] {{RISK_VERYLOW, RISK_VERYLOW, RISK_LOW, RISK_LOW, RISK_MODERATE,},
                                 {RISK_VERYLOW, RISK_LOW, RISK_MODERATE, RISK_MODERATE, RISK_MODERATE,},                    
                                 {RISK_LOW, RISK_MODERATE, RISK_MODERATE, RISK_MODERATE, RISK_HIGH,},
                                 {RISK_LOW, RISK_MODERATE, RISK_MODERATE, RISK_HIGH, RISK_CRITICAL,},
                                 {RISK_MODERATE, RISK_MODERATE, RISK_HIGH, RISK_CRITICAL, RISK_CRITICAL,}
                                 };

        public List<LOV> ListEventStatus()
        {
            return new List<LOV>()
            {
                new LOV("EventStatus", "Completed", "CMP"),
                new LOV("EventStatus", "Schedule Confirmed", "SC"),
                new LOV("EventStatus", "RFQ in progress", "RIP"),
                new LOV("EventStatus", "Tentative", "T")
            };
        }
    }


}
