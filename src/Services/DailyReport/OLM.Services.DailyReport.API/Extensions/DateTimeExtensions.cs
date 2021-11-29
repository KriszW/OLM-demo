using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetDateOfTheStartOfWeek(this DateTime value, DayOfWeek firstDay)
        {
            var output = value.Date;

            while (output.DayOfWeek != firstDay)
            {
                output = output.AddDays(-1);
            }

            return output.Date;
        }


        public static DateTime GetDateOfTheEndOfWeek(this DateTime value, DayOfWeek lastDay)
        {
            var output = value.Date;

            while (output.DayOfWeek != lastDay)
            {
                output = output.AddDays(1);
            }

            return output.Date.Add(new TimeSpan(23, 59, 59));
        }
    }
}
