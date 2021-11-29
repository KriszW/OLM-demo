using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.CategoryBulbs.API.Models;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.ControllerBases;

namespace OLM.Services.CategoryBulbs.API.Controllers
{
    [Route("/api/category/itemnumber/manager/file")]
    [Authorize]
    [ApiController]
    public class LamellaItemnumberFileUploaderController : ModelFileUploaderControllerBase<ItemnumberCategoryModel>
    {
        public LamellaItemnumberFileUploaderController(ICSVDataManager<ItemnumberCategoryModel> csvDataManager) : base(csvDataManager, "lamella-categories.csv")
        {
        }
    }
}
