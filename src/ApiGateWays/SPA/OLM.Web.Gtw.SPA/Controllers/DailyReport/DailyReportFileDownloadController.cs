using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.ApiGateWays.Web.Gtw.SPA.Services.Aggregators.DailyReport.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Controllers.DailyReport
{
    [Route("api/dailyreports/files")]
    [ApiController]
    public class DailyReportFileDownloadController : ControllerBase
    {
        private readonly IDailyReportFileDownloaderAggregator _dailyReportFileDownloaderRepository;

        public DailyReportFileDownloadController(IDailyReportFileDownloaderAggregator dailyReportFileDownloaderRepository)
        {
            _dailyReportFileDownloaderRepository = dailyReportFileDownloaderRepository;
        }

        [HttpGet]
        [Route("dimension/daily")]
        public async Task<IActionResult> DailyDimension([FromQuery] DateTime date)
        {
            var fileData = await _dailyReportFileDownloaderRepository.DimensionDaily(date);

            if (fileData != default)
            {
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-napidimenzió.xlsx");
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {date} naphoz nem lehet létrehozni a reportot"));
            }
        }

        [HttpGet]
        [Route("dimension/weekly")]
        public async Task<IActionResult> WeeklyDimension([FromQuery] DateTime date)
        {
            var fileData = await _dailyReportFileDownloaderRepository.DimensionWeek(date);

            if (fileData != default)
            {
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-hetidimenzió.xlsx");
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {date} naphoz nem lehet létrehozni a reportot"));
            }
        }

        [HttpGet]
        [Route("weekly")]
        public async Task<IActionResult> Weekly([FromQuery] DateTime date)
        {
            var fileData = await _dailyReportFileDownloaderRepository.Weekly(date);

            if (fileData != default)
            {
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-heti.xlsx");
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {date} naphoz nem lehet létrehozni a reportot"));
            }
        }

        [HttpGet]
        [Route("weeks")]
        public async Task<IActionResult> Weeks([FromQuery] WeeksRequestViewModel model)
        {
            var fileData = await _dailyReportFileDownloaderRepository.Weeks(model);

            if (fileData != default)
            {
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-éves.xlsx");
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {model.Start} kezdő és {model.End} vég időtartam között nem lehet elkészíteni a report fájlt az éves reporthoz"));
            }
        }
    }
}
