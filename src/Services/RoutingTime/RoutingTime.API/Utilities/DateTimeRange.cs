using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.RoutingTime.API.Utilities
{
    public class DateTimeRange
    {
        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool Intersects(DateTimeRange other)
        {
            if (this.Start > this.End || other.Start > other.End)
                return false;

            if (this.Start == this.End || other.Start == other.End)
                return false;

            if (this.Start == other.Start || this.End == other.End)
                return true;

            if (this.Start < other.Start)
            {
                if (this.End > other.Start && this.End < other.End)
                    return true;

                if (this.End > other.End)
                    return true;
            }
            else
            {
                if (other.End > this.Start && other.End < this.End)
                    return true;

                if (other.End > this.End)
                    return true;
            }

            return false;
        }
    }
}
