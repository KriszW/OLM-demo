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
    public class TramDimensionController : ControllerBase
    {
        private ITramDimensionRepository _tramDimensionRepository;

        public TramDimensionController(ITramDimensionRepository tramDimensionRepository)
        {
            _tramDimensionRepository = tramDimensionRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<TramDimensionModel>>>> GetPaginated(
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _tramDimensionRepository.GetPagineted(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<TramDimensionModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<TramDimensionModel>>($"A megadott adatokkal nincs feltöltve csille dimenzió az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<APIResponse<IEnumerable<string>>>> GetAllDimension()
        {
            var data = await _tramDimensionRepository.GetAllDimension();

            if (data != default && data.Any() == true)
            {
                return Ok(new APIResponse<IEnumerable<string>>(data));
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<string>>("Nincs dimenzió feltöltve az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<TramDimensionModel>>> GetPriceByID([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int id)
        {
            var data = await _tramDimensionRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<TramDimensionModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<TramDimensionModel>(new APIError(nameof(id), $"A {id} azonosítóval nincs feltöltve csille dimenzió az adatbázisba")));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Add([FromBody] TramDimensionModel model)
        {
            try
            {
                await _tramDimensionRepository.Add(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval már van csille dimenzió a rendszerben")));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' dimenzió már fel van töltve az adatbázisba")));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID,
                                                                 [FromBody] TramDimensionModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _tramDimensionRepository.Modify(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik csille dimenzió a rendszerben")));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' dimenzióval már van feltöltve csille dimenzió az adatbázisba")));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID)
        {
            try
            {
                await _tramDimensionRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik csille dimenzió a rendszerben")));
            }
        }
    }
}
