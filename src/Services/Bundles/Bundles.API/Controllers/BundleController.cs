using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.API.Extensions;
using OLM.Services.Bundles.API.Models;
using OLM.Services.Bundles.API.Services.Repositories.Abstractions.Bundle;
using OLM.Services.Bundles.API.Services.Services.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.Bundles.APIResponses;
using OLM.Shared.Models.Bundles.APIResponses.OneMachine;
using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;

namespace OLM.Services.Bundles.API.Controllers
{
    [Route("api/bundle")]
    [ApiController]
    public class BundleController : ControllerBase
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly ITCOBundleRepository _tcoBundleRepository;
        private readonly ITCOBundleFileWriterService _tcoBundleFileWriterService;

        public BundleController(IBundleRepository bundleRepository,
                                ITCOBundleRepository tcoBundleRepository = default,
                                ITCOBundleFileWriterService tcoBundleFileWriterService = default)
        {
            _bundleRepository = bundleRepository;
            _tcoBundleRepository = tcoBundleRepository;
            _tcoBundleFileWriterService = tcoBundleFileWriterService;
        }

        [HttpGet]
        [Route("tco/download")]
        public async Task<IActionResult> DownloadTCOBundleData([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var data = await _tcoBundleRepository.FetchData(from, to);


            if (data != default)
            {
                var fileStream = await _tcoBundleFileWriterService.WriteToFile(data);

                if (fileStream != default)
                {
                    if (fileStream is MemoryStream memStream)
                    {
                        return File(memStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"tco-data-{from:s}-{to:s}.xlsx");
                    }
                    else
                    {
                        return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"tco-data-{from:s}-{to:s}.xlsx");
                    }
                }
                else
                {
                    return NotFound(new EmptyAPIResponse($"A {from}-tól {to}-ig nem lehet létrehozni a fájlt"));
                }
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<TCOBundleAPIResponseViewModel>>($"A {from} dátumtól {to}-ig nincs új adat"));
            }
        }

        [HttpGet]
        [Route("tco/list")]
        public async Task<ActionResult<APIResponse<Paginated<TCOBundleAPIResponseViewModel>>>> GetTCOBundle([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var model = await _tcoBundleRepository.FetchData(from, to, pageIndex, pageSize);

            if (model != default)
            {
                return Ok(new APIResponse<Paginated<TCOBundleAPIResponseViewModel>>(model));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<TCOBundleAPIResponseViewModel>>($"A {from} dátumtól {to}-ig nincs új adat"));
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<APIResponse<List<BundleModel>>>> GetLatestBundle([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var model = await _bundleRepository.GetBundles(from, to);

            if (model != default)
            {
                return Ok(new APIResponse<List<BundleModel>>(model));
            }
            else
            {
                return NotFound(new APIResponse<List<BundleModel>>($"A {from} dátumtól {to}-ig nincs új adat"));
            }
        }

        [HttpGet]
        [Route("latest/{machineName}")]
        public async Task<ActionResult<APIResponse<BundleAPIResponseViewModel>>> GetLatestBundle([FromRoute] string machineName)
        {
            var model = await _bundleRepository.GetLatestForMachine(machineName);

            if (model != default)
            {
                return Ok(new APIResponse<BundleAPIResponseViewModel>(model.ConvertToViewModel()));
            }
            else
            {
                return NotFound(new APIResponse<BundleAPIResponseViewModel>($"A '{machineName}' géphez nincs feltöltve a legutolsó rakat"));
            }
        }

        [HttpGet]
        [Route("daily/{machineName}")]
        public async Task<ActionResult<APIResponse<BundleDailyAPIResponseViewModel>>> GetDailyBundles([FromRoute] string machineName)
        {
            var data = await _bundleRepository.GetDailyBundlesForMachine(machineName);

            if (data != default && data.Count > 0)
            {
                var models = data.Select(b => b.ConvertToViewModel());

                return Ok(new APIResponse<BundleDailyAPIResponseViewModel>(new BundleDailyAPIResponseViewModel(models)));
            }
            else
            {
                return NotFound(new APIResponse<BundleDailyAPIResponseViewModel>($"A '{machineName}' géphez nincs feltöltve a napi rakatok adatai"));
            }
        }

        [HttpGet]
        [Route("weekly/{machineName}")]
        public async Task<ActionResult<APIResponse<BundleWeeklyAPIResponseViewModel>>> GetWeeklyBundles([FromRoute] string machineName)
        {
            var data = await _bundleRepository.GetWeeklyBundlesForMachine(machineName);

            if (data != default && data.Count > 0)
            {
                var models = data.Select(b => b.ConvertToViewModel());

                return Ok(new APIResponse<BundleWeeklyAPIResponseViewModel>(new BundleWeeklyAPIResponseViewModel(models)));
            }
            else
            {
                return NotFound(new APIResponse<BundleWeeklyAPIResponseViewModel>($"A '{machineName}' géphez nincs feltöltve a heti rakatok adatai"));
            }
        }
    }
}