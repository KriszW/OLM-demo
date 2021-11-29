using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Extensions
{
    public static class DoubleExtensions
    {
        public static double RoundTo0DecimalDigits(this double number)
            => number.RoundNumber(0);

        public static double RoundTo2DecimalDigits(this double number)
            => number.RoundNumber(2);

        public static double RoundNumber(this double number, int digits)
            => Math.Round(number, digits);
    }
}
