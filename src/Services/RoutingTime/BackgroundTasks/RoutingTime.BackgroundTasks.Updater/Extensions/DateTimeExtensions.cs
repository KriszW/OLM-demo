using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Extensions
{
    public static class DateTimeExtensions
    {
        public static IEnumerable<DateTime> GetDaysUntilTheStartOfTheWeek(this DateTime value, DayOfWeek firstDay)
        {
            var output = new List<DateTime>();

            var date = value.Date;
            output.Add(date);

            while (date.DayOfWeek != firstDay)
            {
                date = date.AddDays(-1);
                output.Add(date);
            }

            return output;
        }
    }
}
