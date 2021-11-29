using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.ViewModels
{
    public class TargetResponseViewModel
    {
        public string Dimension { get; set; }

        public double Target { get; set; }

        public double Intersection { get; set; }
    }
}
