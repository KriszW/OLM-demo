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
    public class ProductionTimeController : ControllerBase
    {
        private readonly IProductionTimeRepository _productionTimeRepository;

        public ProductionTimeController(IProductionTimeRepository productionTimeRepository)
        {
            _productionTimeRepository = productionTimeRepository;
        }

        [HttpGet]
        [Route("list/weeks")]
        public async Task<ActionResult<APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>>> GetWeekNumberPaginated(
        [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
        [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _productionTimeRepository.GetPaginatedForWeekNumber(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<WeekNumberPaginatorModelDataViewModel>>($"A megadott adatokkal nincs feltöltve nyitva tartási idő ezeken a heteken az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("list/data")]
        public async Task<ActionResult<APIResponse<Paginated<ProductionTimeModel>>>> GetDataPaginated(
            [FromQuery] int year,
            [FromQuery] int weekNumber,
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _productionTimeRepository.GetPaginatedForData(year, weekNumber, skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<ProductionTimeModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<ProductionTimeModel>>($"A megadott adatokkal nincs feltöltve nyitva tartási idő az adatbázisba"));
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<ProductionTimeModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int id)
        {
            var data = await _productionTimeRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<ProductionTimeModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<ProductionTimeModel>(new APIError(nameof(id), $"A {id} azonosítóval nincs feltöltve nyitva tartási idő az adatbázisba")));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Add([FromBody] ProductionTimeModel model)
        {
            try
            {
                await _productionTimeRepository.Add(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval már van nyitva tartási idő feltöltve a rendszerben")));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID,
                                                                    [FromBody] ProductionTimeModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _productionTimeRepository.Modify(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik nyitva tartási idő a rendszerben")));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID)
        {
            try
            {
                await _productionTimeRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik nyitva tartási idő a rendszerben")));
            }
        }
    }
}
