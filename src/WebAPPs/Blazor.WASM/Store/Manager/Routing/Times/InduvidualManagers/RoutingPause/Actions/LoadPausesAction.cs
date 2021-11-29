using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingPause.Actions
{
    public class LoadPausesAction
    {
        public LoadPausesAction(int pageIndex,
                                int pageSize,
                                int weekNumber,
                                int year)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            WeekNumber = weekNumber;
            Year = year;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int WeekNumber { get; set; }

        public int Year { get; set; }
    }
}
