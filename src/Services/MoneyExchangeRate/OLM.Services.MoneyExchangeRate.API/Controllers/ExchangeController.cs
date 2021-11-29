using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Services.Abstractions;
using OLM.Services.MoneyExchangeRate.API.Services.Services.Implementations;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;

namespace OLM.Services.MoneyExchangeRate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public ExchangeController(ICurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }

        [HttpGet]
        [Route("{isoCode}")]
        public async Task<ActionResult<APIResponse<CurrencyModel>>> GetByISOCode(string isoCode)
        {
            var data = await _currencyExchangeService.GetByISOCode(isoCode);

            if (data != default)
            {
                return Ok(new APIResponse<CurrencyModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<CurrencyModel>($"Nincs valuta feltöltve '{isoCode}' ISO kóddal"));
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<APIResponse<CurrencyExchangedDataViewModel>>> Exchange([FromQuery] ExchangeCurrencyViewModel model)
        {
            var data = await _currencyExchangeService.Exchange(model);

            return Ok(new APIResponse<CurrencyExchangedDataViewModel>(new CurrencyExchangedDataViewModel { Rate = data }));
        }
    }
}
