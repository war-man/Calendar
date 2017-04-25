using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.CalendarViewModels
{
    public class EventIndexData
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Acknowledgement> Acknowledgements { get; set; }
    }
}
