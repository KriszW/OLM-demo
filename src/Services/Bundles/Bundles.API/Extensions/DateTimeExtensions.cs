using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetDateOfThisWeekBackward(this DateTime date, DayOfWeek destination)
        {
            date = GetTheStartOfTheDate(date);

            while (date.DayOfWeek != destination)
            {
                date = date.AddDays(-1);
            }

            return date;
        }
        public static DateTime GetDateOfThisWeekForward(this DateTime date, DayOfWeek destination)
        {
            date = GetTheStartOfTheDate(date);

            while (date.DayOfWeek != destination)
            {
                date = date.AddDays(1);
            }

            return date;
        }

        private static DateTime GetTheStartOfTheDate(DateTime date) => date.Date;
    }
}