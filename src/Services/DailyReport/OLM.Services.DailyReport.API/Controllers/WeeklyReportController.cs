using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Weekly;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;

namespace OLM.Services.DailyReport.API.Controllers
{
    [Route("api/report/weekly")]
    [ApiController]
    public class WeeklyReportController : ControllerBase
    {
        private readonly IWeeklyReportFetchService _weeklyReportFetchService;

        public WeeklyReportController(IWeeklyReportFetchService weeklyReportFetchService)
        {
            _weeklyReportFetchService = weeklyReportFetchService;
        }

        [HttpGet]
        [Route("fetch")]
        public async Task<ActionResult<APIResponse<WeeklyReportResponseViewModel>>> FetchWeekly([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                                                                           [FromQuery] DateTime? date = default)
        {
            if (date == default) date = DateTime.Today;

            var data = await _weeklyReportFetchService.FetchWeekly(date.Value, tramModels);

            if (data != default && data.Models?.Any() == true)
            {
                return Ok(new APIResponse<WeeklyReportResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<WeeklyReportResponseViewModel>($"A {date} időponthoz nem található adat az adatbázisban"));
            }
        }
    }
}
