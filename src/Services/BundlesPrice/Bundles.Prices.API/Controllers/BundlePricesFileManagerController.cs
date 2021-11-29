using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.ControllerBases;
using System.Threading.Tasks;
using System;
using OLM.Services.Bundles.Prices.API.Services.Services.Abstractions;

namespace OLM.Services.Bundles.Prices.API.Controllers
{
    [Route("api/bundle/prices/manager/file")]
    //[Authorize]
    [ApiController]
    public class BundlePricesFileManagerController : ControllerBase
    {
        private readonly IBundlePriceCsvDataManager _csvDataManager;

        public BundlePricesFileManagerController(IBundlePriceCsvDataManager csvDataManager)
        {
            _csvDataManager = csvDataManager;
        }

        [Route("upload")]
        [HttpPost]
        public virtual async Task<ActionResult<EmptyAPIResponse>> UploadAsync([FromQuery] bool deleteAll, [FromQuery] string destinationISOCode, IFormFile file)
        {
            if (deleteAll) await _csvDataManager.UploadWithDeleteAllAsync(file, destinationISOCode);
            else await _csvDataManager.UploadAsync(file, destinationISOCode);

            return Ok(new EmptyAPIResponse());
        }

        [Route("download")]
        [HttpGet]
        public virtual async Task<IActionResult> DownloadAsync()
        {
            var fileData = await _csvDataManager.DownloadAsync();

            return File(fileData, "text/csv", "bundle-prices.csv");
        }
    }
}
