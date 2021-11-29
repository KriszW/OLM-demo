using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.ViewModels
{
    public class SummarizedDailyReportDataViewModel
    {
        public SummarizedDailyReportDataViewModel(double allLength, double allWaste, double allFS)
        {
            AllLength = allLength;
            AllWaste = allWaste;
            AllFS = allFS;
        }

        public double AllLength { get; set; }
        public double AllWaste { get; set; }
        public double AllFS { get; set; }

    }
}
