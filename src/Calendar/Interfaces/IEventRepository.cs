using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Models;

namespace Calendar.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> List();
    }
}
