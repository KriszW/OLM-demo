using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.DailyReport.BackgroundTasks.Updater.Models
{
    public class DailyReportModel
    {
        public DateTime Date { get; set; }
        public string Dimension { get; set; }
        public double Length { get; set; }
        public double Waste { get; set; }
        public double FS { get; set; }
    }
}
