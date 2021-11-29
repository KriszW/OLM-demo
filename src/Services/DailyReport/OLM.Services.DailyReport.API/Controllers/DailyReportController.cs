using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.Daily;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;

namespace OLM.Services.DailyReport.API.Controllers
{
    [Route("api/report/daily")]
    [ApiController]
    public class DailyReportController : ControllerBase
    {
        private readonly IDailyReportDataFetchService _dailyReportDataFetchService;

        public DailyReportController(IDailyReportDataFetchService dailyReportDataFetchService)
        {
            _dailyReportDataFetchService = dailyReportDataFetchService;
        }

        [HttpGet]
        [Route("day/fetch")]
        public async Task<ActionResult<APIResponse<DimensionReportSummarizedResponseViewModel>>> FetchDaily([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                                                                                   [FromQuery] DateTime? date = default)
        {
            if (date == default) date = DateTime.Now;

            var data = await _dailyReportDataFetchService.FetchDaily(date.Value, tramModels);

            if (data != default)
            {
                return Ok(new APIResponse<DimensionReportSummarizedResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<DimensionReportSummarizedResponseViewModel>($"A {date} időponthoz nem lehet lekérdezni a dimenzió selejt napi összesítését"));
            }
        }

        [HttpGet]
        [Route("week/fetch")]
        public async Task<ActionResult<APIResponse<DimensionReportSummarizedResponseViewModel>>> FetchWeekly([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                                                                           [FromQuery] DateTime? date = default)
        {
            if (date == default) date = DateTime.Today;

            var data = await _dailyReportDataFetchService.FetchWeekly(date.Value, tramModels);

            if (data != default)
            {
                return Ok(new APIResponse<DimensionReportSummarizedResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<DimensionReportSummarizedResponseViewModel>($"A {date} időponthoz nem lehet lekérdezni a dimenzió selejt heti összesítését"));
            }
        }

    }
}
