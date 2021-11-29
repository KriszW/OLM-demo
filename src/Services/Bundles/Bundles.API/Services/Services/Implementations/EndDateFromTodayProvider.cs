using OLM.Services.Bundles.API.Extensions;
using OLM.Services.Bundles.API.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Services.Implementations
{
    public class EndDateFromTodayProvider : IEndDateProvider
    {
        private static readonly TimeSpan endTime = new(23, 59, 59);

        public DateTime GetEndOfTheDay() => DateTime.Now.Date.Add(endTime);

        public DateTime GetEndOfTheWeek() => DateTime.Now.GetDateOfThisWeekForward(DayOfWeek.Sunday).Add(endTime);
    }
}
