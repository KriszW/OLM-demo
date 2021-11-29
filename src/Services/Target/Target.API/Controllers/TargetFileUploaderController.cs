using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Target.API.Models;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.ControllerBases;

namespace OLM.Services.Target.API.Controllers
{
    [Route("/api/category/itemnumber/manager/file")]
    [Authorize]
    [ApiController]
    public class TargetFileUploaderController : ModelFileUploaderControllerBase<WasteTargetDataModel>
    {
        public TargetFileUploaderController(ICSVDataManager<WasteTargetDataModel> csvDataManager) : base(csvDataManager, "lamella-categories.csv")
        {
        }
    }
}
