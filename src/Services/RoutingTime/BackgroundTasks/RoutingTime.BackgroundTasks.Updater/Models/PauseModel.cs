using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.RoutingTime.BackgroundTasks.Updater.Models
{
    public class PauseModel
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DayOfWeek Day { get; set; }

        public int WeekNumber { get; set; }

        public string MachineName { get; set; }
    }
}
