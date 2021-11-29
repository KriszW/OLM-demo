using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetPreviousWorkDay(this DateTime date)
        {
            if(date.DayOfWeek == DayOfWeek.Monday) return date.AddDays(-3);
            if(date.DayOfWeek == DayOfWeek.Sunday) return date.AddDays(-2);
            else return date.AddDays(-1);
        }
    }
}
