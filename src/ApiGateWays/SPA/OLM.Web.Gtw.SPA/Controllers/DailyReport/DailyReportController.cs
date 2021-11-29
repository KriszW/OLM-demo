using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Response.Report;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Controllers.DailyReport
{
    [Route("api/dailyreports/")]
    [ApiController]
    public class DailyReportController : ControllerBase
    {
        private readonly IDailyReportAggregator _dailyReportAggregator;

        public DailyReportController(IDailyReportAggregator dailyReportAggregator)
        {
            _dailyReportAggregator = dailyReportAggregator;
        }

        [HttpGet]
        [Route("dimension/day")]
        public async Task<ActionResult<APIResponse<DimensionReportSummarizedResponseViewModel>>> FetchDimensionForDay([FromQuery] DateTime date)
        {
            var data = await _dailyReportAggregator.FetchDimensionForDay(date);

            if (data != default)
            {
                return Ok(new APIResponse<DimensionReportSummarizedResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<DimensionReportSummarizedResponseViewModel>($"A {date} időponthoz nem található adat a napi dimenzió selejt reporthoz"));
            }
        }

        [HttpGet]
        [Route("dimension/week")]
        public async Task<ActionResult<APIResponse<DimensionReportSummarizedResponseViewModel>>> FetchDimensionForWeek([FromQuery] DateTime date)
        {
            var data = await _dailyReportAggregator.FetchDimensionForWeek(date);

            if (data != default)
            {
                return Ok(new APIResponse<DimensionReportSummarizedResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<DimensionReportSummarizedResponseViewModel>($"A {date} időponthoz nem található adat a heti dimenzió selejt reporthoz"));
            }
        }

        [HttpGet]
        [Route("weekly")]
        public async Task<ActionResult<APIResponse<WeeklyReportResponseViewModel>>> FetchForWeeklyDay([FromQuery] DateTime date)
        {
            var data = await _dailyReportAggregator.FetchWeeklyReport(date);

            if (data != default)
            {
                return Ok(new APIResponse<WeeklyReportResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<WeeklyReportResponseViewModel>($"A {date} időponthoz nem található adat a heti összesítő selejt reporthoz"));
            }
        }

        [HttpGet]
        [Route("weeks")]
        public async Task<ActionResult<APIResponse<WeeksReportResponseViewModel>>> FetchForYearlyWeeks([FromQuery] WeeksRequestViewModel model)
        {
            var data = await _dailyReportAggregator.FetchYearlyWeeks(model);

            if (data != default)
            {
                return Ok(new APIResponse<WeeksReportResponseViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<WeeksReportResponseViewModel>($"A {model.Start} kezdő és {model.End} vég időtartam között nem található adat a heti összesítés selejt reporthoz"));
            }
        }
    }
}
