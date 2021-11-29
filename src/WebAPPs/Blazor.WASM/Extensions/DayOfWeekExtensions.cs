using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static string ToLocalizedName(this DayOfWeek day) => day switch
        {
            DayOfWeek.Monday => "Hétfő",
            DayOfWeek.Tuesday => "Kedd",
            DayOfWeek.Wednesday => "Szerda",
            DayOfWeek.Thursday => "Csütörtök",
            DayOfWeek.Friday => "Péntek",
            DayOfWeek.Saturday => "Szombat",
            DayOfWeek.Sunday => "Vasárnap",
            _ => "Ismeretlen",
        };
    }
}
