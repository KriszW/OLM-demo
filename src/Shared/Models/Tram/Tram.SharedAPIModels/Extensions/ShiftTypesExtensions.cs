using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.Tram.SharedAPIModels.Extensions
{
    public static class ShiftTypesExtensions
    {
        public static ShiftTypes GetShiftTypeForTime(this DateTime date)
        {
            var time = date.TimeOfDay;

            if (time.Hours >= 6 && time.Hours < 14)
            {
                return ShiftTypes.De;
            }
            else if(time.Hours >= 14 && time.Hours < 22)
            {
                return ShiftTypes.Du;
            }
            else
            {
                return ShiftTypes.Ej;
            }
        }
    }
}
