using OLM.Services.DailyReport.API.Models;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.ViewModels
{
    public class ModelAggregatorViewModel
    {
        public IEnumerable<DailyReportDataModel> Data { get; set; }

        public DailyReportRequestTramViewModel TramModel { get; set; }

        public TargetResponseViewModel TargetModel { get; set; }
    }
}
