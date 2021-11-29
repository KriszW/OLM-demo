using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.MoneyExchangeRate.API.Models;
using OLM.Services.MoneyExchangeRate.API.Services.Repositories.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;

namespace OLM.Services.MoneyExchangeRate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyExchangeRateController : ControllerBase
    {
        private readonly IMoneyExchangeRateRepository _moneyExchangeRateRepository;

        public MoneyExchangeRateController(IMoneyExchangeRateRepository moneyExchangeRateRepository)
        {
            _moneyExchangeRateRepository = moneyExchangeRateRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<APIResponse<List<string>>>> All()
        {
            var data = await _moneyExchangeRateRepository.All();

            if (data != default && data.Any() == true)
            {
                return Ok(new APIResponse<List<string>>(data));
            }
            else
            {
                return NotFound(new APIResponse<List<string>>($"Nincs feltöltve valuta az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<CurrencyModel>>>> Paginate(
                [FromQuery] int pageIndex = 0,
                [FromQuery] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _moneyExchangeRateRepository.GetPaginated(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<CurrencyModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<CurrencyModel>>($"A megadott adatokkal nincs feltöltve valuta az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<CurrencyModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int id)
        {
            var data = await _moneyExchangeRateRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<CurrencyModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<CurrencyModel>($"A {id} azonosítóval nincs feltöltve valuta az adatbázisba"));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Add([FromBody] CurrencyModel model)
        {
            try
            {
                await _moneyExchangeRateRepository.Add(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval már létezik valuta a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} valutával már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpPut]
        [Route("modify/{id}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int id, [FromBody] CurrencyModel model)
        {
            if (id != model.ID)
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a megadott ID nem egyezik"));
            }

            try
            {
                await _moneyExchangeRateRepository.Modify(id,model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik valuta a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} valutával már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int id)
        {
            try
            {
                await _moneyExchangeRateRepository.Delete(id);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik valuta a rendszerben"));
            }
        }
    }
}
