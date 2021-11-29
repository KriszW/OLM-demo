using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.DailyReport.API.Services.Services.Abstractions.FileManager;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Report;
using OLM.Shared.Models.DailyReport.SharedAPIModels.Request.Tram;

namespace OLM.Services.DailyReport.API.Controllers
{
    [Route("api/report/files")]
    [ApiController]
    public class ReportFileContoller : ControllerBase
    {
        private readonly IDailyReportFileWriter _dailyReportFileWriter;
        private readonly IWeeklyReportFileWriter _weeklyReportFileWriter;
        private readonly IWeeksReportFileWriter _weeksReportFileWriter;

        public ReportFileContoller(IDailyReportFileWriter dailyReportFileWriter,
                                   IWeeklyReportFileWriter weeklyReportFileWriter,
                                   IWeeksReportFileWriter weeksReportFileWriter)
        {
            _dailyReportFileWriter = dailyReportFileWriter;
            _weeklyReportFileWriter = weeklyReportFileWriter;
            _weeksReportFileWriter = weeksReportFileWriter;
        }

        [HttpGet]
        [Route("dimension/daily")]
        public async Task<IActionResult> DailyDimension([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                                        [FromQuery] DateTime? date = default)
        {
            if (date.HasValue == false) date = DateTime.Now;

            var fileMemoryStream = await _dailyReportFileWriter.CreateDimensionForDailyFile(date.Value, tramModels);

            if(fileMemoryStream != default)
            {
                if (fileMemoryStream is MemoryStream memStream)
                {
                    return File(memStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-dim-daily.xlsx");
                }
                else
                {
                    return File(fileMemoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-dim-daily.xlsx");
                }
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {date} naphoz nem lehet létrehozni a reportot"));
            }
        }

        [HttpGet]
        [Route("dimension/weekly")]
        public async Task<IActionResult> WeeklyDimension([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                                         [FromQuery] DateTime? date = default)
        {
            if (date.HasValue == false) date = DateTime.Now;

            var fileMemoryStream = await _dailyReportFileWriter.CreateDimensionForWeekFile(date.Value, tramModels);

            if (fileMemoryStream != default)
            {
                if (fileMemoryStream is MemoryStream memStream)
                {
                    return File(memStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-dim-weekly.xlsx");
                }
                else
                {
                    return File(fileMemoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-dim-weekly.xlsx");
                }
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {date} héthez nem lehet létrehozni a dimenzió reportot"));
            }
        }

        [HttpGet]
        [Route("weekly")]
        public async Task<IActionResult> Weekly([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                                [FromQuery] DateTime? date = default)
        {
            if (date.HasValue == false) date = DateTime.Now;

            var fileMemoryStream = await _weeklyReportFileWriter.CreateWeeklyFile(date.Value, tramModels);

            if (fileMemoryStream != default)
            {
                if (fileMemoryStream is MemoryStream memStream)
                {
                    return File(memStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-weekly.xlsx");
                }
                else
                {
                    return File(fileMemoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-weekly.xlsx");
                }
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {date} héthez nem lehet létrehozni a hetek napi reportját"));
            }
        }

        [HttpGet]
        [Route("weeks")]
        public async Task<IActionResult> Weeks([FromBody] IEnumerable<DailyReportRequestTramViewModel> tramModels,
                                               [FromQuery] WeeksRequestViewModel model)
        {
            var fileMemoryStream = await _weeksReportFileWriter.CreateWeeksFile(model, tramModels);

            if (fileMemoryStream != default)
            {
                if (fileMemoryStream is MemoryStream memStream)
                {
                    return File(memStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-weeks.xlsx");
                }
                else
                {
                    return File(fileMemoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-weeks.xlsx");
                }
            }
            else
            {
                return NotFound(new EmptyAPIResponse($"A {model.Start} kezdő és {model.End} vég időtartam között nem lehet elkészíteni a report fájlt az éves reporthoz"));
            }
        }
    }
}
