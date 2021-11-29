using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.DailyReport.API.Services.Services.Abstractions;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weeks;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;

namespace OLM.Services.DailyReport.API.Controllers
{
    [Route("api/report/weeks")]
    [ApiController]
    public class WeeksReportController : ControllerBase
    {
        private readonly IWeeksReportFetchService _weeksReportFetchService;

        public WeeksReportController(IWeeksReportFetchService weeksReportFetchService)
        {
            _weeksReportFetchService = weeksReportFetchService;
        }

        [HttpGet]
        [Route("fetch")]
        public async Task<ActionResult<APIResponse<WeeksReportResponseViewModel>>> FetchWeekly([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                                                                           [FromQuery] WeeksRequestViewModel model = default)
        {
            if (model == default) model = new WeeksRequestViewModel(new DateTime(DateTime.Now.Year,1,1),DateTime.Now);

            var data = await _weeksReportFetchService.Fetch(model, tramModels);

            if (data != default && data.Data?.Any() == true)
            {
                return Ok(new APIResponse<WeeksReportResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<WeeksReportResponseViewModel>($"A {model.Start}-től a {model.End}-ig nem található adat az adatbázisban"));
            }
        }
    }
}
