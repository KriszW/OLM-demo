using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.TCO.API.Models;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.ControllerBases;

namespace OLM.Services.TCO.API.Controllers
{
    [Route("/api/tco/manager/file")]
    [Authorize]
    [ApiController]
    public class TCOFileUploaderController : ModelFileUploaderControllerBase<TCOValueSettingsModel>
    {
        public TCOFileUploaderController(ICSVDataManager<TCOValueSettingsModel> csvDataManager) : base(csvDataManager, "tco-settings.csv")
        {
        }
    }
}
