using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Calendar.Models;
using Calendar.Models.CalendarViewModels;

namespace Calendar.Helpers
{
    public class PaginatedEventIndex : EventIndexData
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }

        public PaginatedEventIndex(EventIndexData source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Events.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            
            
            var Events = source.Events.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            this.Events = Events;
            /* EF will filter Acknowledgemnts for us!? */
            //this.Acknowledgements = source.Acknowledgements.Where(a => Events.All(e => e.ID == a.EventID));
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        /* The CreateAsync method takes page size and page number and applies the appropriate 
         * Skip and Take statements to the IQueryable. */
         /*
        public Task<PaginatedEventIndex> CreateAsync(EventIndexData source, int pageIndex, int pageSize)
        {
            var count = source.Events.Count();
            var items = source.Events.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedEventIndex(items, count, pageIndex, pageSize);
        }
        */
    }
}
