using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.RoutingTime.API.Models;
using OLM.Services.RoutingTime.API.Services.Repositories.Abstractions;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;
using OLM.Shared.Models.RoutingTime.SharedAPIModels.Response.DataModel;

namespace OLM.Services.RoutingTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PausesController : ControllerBase
    {
        private readonly IPausesRepository _pausesRepository;

        public PausesController(IPausesRepository pausesRepository)
        {
            _pausesRepository = pausesRepository;
        }

        [HttpGet]
        [Route("list/weeks")]
        public async Task<ActionResult<APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>>> GetWeekNumberPaginated(
        [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
        [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _pausesRepository.GetPaginatedForWeekNumber(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>($"A megadott adatokkal nincs feltöltve szünet ezeken a heteken az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("list/data")]
        public async Task<ActionResult<APIResponse<Paginated<PauseModel>>>> GetDataPaginated(
            [FromQuery] int year,
            [FromQuery] int weekNumber,
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _pausesRepository.GetPaginatedForData(year, weekNumber, skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<PauseModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<PauseModel>>($"A megadott adatokkal nincs feltöltve szünet az adatbázisba"));
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<PauseModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int id)
        {
            var data = await _pausesRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<PauseModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<PauseModel>(new APIError(nameof(id), $"A {id} azonosítóval nincs feltöltve szünet az adatbázisba")));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Add([FromBody] PauseModel model)
        {
            try
            {
                await _pausesRepository.Add(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval már van szünet feltöltve a rendszerben")));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID,
                                                                    [FromBody] PauseModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _pausesRepository.Modify(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik szünet a rendszerben")));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID)
        {
            try
            {
                await _pausesRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik szünet a rendszerben")));
            }
        }
    }
}
