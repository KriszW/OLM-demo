using OLM.Services.Bundles.API.Services.Services.Abstractions;
using OLM.Services.Bundles.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Services.Implementations
{
    public class StartDateFromTodayProvider : IStartDateProvider
    {
        private static readonly TimeSpan StartOfWork = new(0, 0, 0);

        public DateTime GetStartDateForDay() => DateTime.Now.Date.Add(StartOfWork);

        public DateTime GetStartDateForWeek() => DateTime.Now.GetDateOfThisWeekBackward(DayOfWeek.Monday).Add(StartOfWork);
    }
}
