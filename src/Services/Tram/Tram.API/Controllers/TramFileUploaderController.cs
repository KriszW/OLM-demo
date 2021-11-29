using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Tram.API.Models;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.ControllerBases;

namespace OLM.Services.Tram.API.Controllers
{
    [Route("/api/tram/manager/file")]
    [ApiController]
    public class TramFileUploaderController : ModelFileUploaderControllerBase<TramDataModel>
    {
        public TramFileUploaderController(ICSVDataManager<TramDataModel> csvDataManager) : base(csvDataManager, "tram.csv")
        {
        }
    }
}
