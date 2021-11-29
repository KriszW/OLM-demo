using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.Extensions;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Services.MoneyExchangeRate.API.ViewModels;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.ExchangeRate;

namespace OLM.Services.MoneyExchangeRate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public ExchangeRateController(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<ExchangeRateModel>>>> Paginate(
                [FromQuery] string ISOCode,
                [FromQuery] int pageIndex = 0,
                [FromQuery] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;
            var model = new PaginatedExchangeRateViewModel(ISOCode, skip, pageSize);

            var data = await _exchangeRateRepository.GetPaginatedForISOCode(model);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<ExchangeRateModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<ExchangeRateModel>>($"A megadott adatokkal nincs feltöltve valuta átváltási ráta az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<ExchangeRateModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int id)
        {
            var data = await _exchangeRateRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<ExchangeRateModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<ExchangeRateModel>($"A {id} azonosítóval nincs feltöltve valuta átváltási ráta az adatbázisba"));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Add([FromBody] UploadNewExchangeRateForISOCodeViewModel model)
        {
            try
            {
                await _exchangeRateRepository.Add(model.SourceISOCode, model.Model.ConvertToModel());

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval már létezik valuta átváltási ráta a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} valuta kóddal már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpPut]
        [Route("modify/{sourceISOCode}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] string sourceISOCode, [FromBody] ExchangeRateModel model)
        {
            if (string.IsNullOrEmpty(sourceISOCode))
            {
                return BadRequest(new EmptyAPIResponse("A kezdő valuta kód nem lehet üres"));
            }

            try
            {
                var modifyModel = new ModifyExchangeRateForCurrencyViewModel
                {
                    Data = model,
                    SourceISOCode = sourceISOCode
                };

                await _exchangeRateRepository.Modify(modifyModel);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik valuta átváltási ráta a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} valuta kóddal már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpDelete]
        [Route("delete/{exchangeRateModel}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int exchangeRateModel)
        {
            try
            {
                await _exchangeRateRepository.Delete(exchangeRateModel);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik valuta átváltási ráta a rendszerben"));
            }
        }
    }
}
