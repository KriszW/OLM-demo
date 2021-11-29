using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.ControllerBases
{
    public abstract class ModelFileUploaderControllerBase<TCSVViewModel> : ControllerBase
        where TCSVViewModel : class
    {
        private readonly ICSVDataManager<TCSVViewModel> _csvDataManager;
        private readonly string _downloadedFilePath;

        public ModelFileUploaderControllerBase(ICSVDataManager<TCSVViewModel> csvDataManager, string downloadedFilePath)
        {
            _csvDataManager = csvDataManager;

            if (downloadedFilePath.EndsWith(".csv") == false) throw new ArgumentException($"A {nameof(downloadedFilePath)} fájl névnek '.csv'-vel kel végződnie");

            _downloadedFilePath = downloadedFilePath;
        }

        [Route("upload")]
        [HttpPost]
        public virtual async Task<ActionResult<EmptyAPIResponse>> UploadAsync([FromQuery] bool deleteAll, IFormFile file)
        {
            if (deleteAll) await _csvDataManager.UploadWithDeleteAllAsync(file);
            else await _csvDataManager.UploadAsync(file);

            return Ok(new EmptyAPIResponse());
        }

        [Route("download")]
        [HttpGet]
        public virtual async Task<IActionResult> DownloadAsync()
        {
            var fileData = await _csvDataManager.DownloadAsync();

            return File(fileData, "text/csv", _downloadedFilePath);
        }
    }
}
