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
        public List<LOV> ListSeverities()
        {
            // a few states from USA
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

    }
}
