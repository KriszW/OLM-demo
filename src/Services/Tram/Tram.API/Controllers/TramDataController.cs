using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OLM.Services.Tram.API.Models;
using OLM.Services.Tram.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;

namespace OLM.Services.Tram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramDataController : ControllerBase
    {
        private ITramDataRepository _tramDataRepository;

        public TramDataController(ITramDataRepository tramDataRepository)
        {
            _tramDataRepository = tramDataRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<TramDataModel>>>> GetPaginated(
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _tramDataRepository.GetPaginetedPrices(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<TramDataModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<TramDataModel>>($"A megadott adatokkal nincs feltöltve csille adat az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<TramDataModel>>> GetPriceByID([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int id)
        {
            var data = await _tramDataRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<TramDataModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<TramDataModel>(new APIError(nameof(id), $"A {id} azonosítóval nincs feltöltve csille adat az adatbázisba")));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Add([FromBody] TramDataModel model)
        {
            try
            {
                await _tramDataRepository.Add(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval már létezik csille adat a rendszerben")));
            }
            catch (DataNotFoundWithSpecifiedColumnException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' csille dimenzió nem létezik az adatbázisba")));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID,
                                                                 [FromBody] TramDataModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _tramDataRepository.Modify(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik csille adat a rendszerben")));
            }
            catch (DataNotFoundWithSpecifiedColumnException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' dimenzióval nincs feltöltve csille dimenzió az adatbázisba")));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID)
        {
            try
            {
                await _tramDataRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik csille adat a rendszerben")));
            }
        }
    }
}
