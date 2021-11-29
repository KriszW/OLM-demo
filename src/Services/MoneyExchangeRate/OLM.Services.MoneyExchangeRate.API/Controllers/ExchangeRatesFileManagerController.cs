using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using OLM.Shared.Utilities.ExcelFileManagerUtility.Abstractions;
using OLM.Shared.Utilities.ExcelFileManagerUtility.ControllerBases;

namespace OLM.Services.MoneyExchangeRate.API.Controllers
{
    [Route("api/exchangerate/manager/file")]
    [ApiController]
    public class ExchangeRatesFileManagerController : ModelFileUploaderControllerBase<ExchangeRateCsvViewModel>
    {
        public ExchangeRatesFileManagerController(ICSVDataManager<ExchangeRateCsvViewModel> csvDataManager) : base(csvDataManager, "exchange-rates.csv")
        {
        }
    }
}
