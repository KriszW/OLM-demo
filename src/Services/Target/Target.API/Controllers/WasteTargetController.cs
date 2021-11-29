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
using OLM.Services.Target.API.Models;
using OLM.Services.Target.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;

namespace OLM.Services.Target.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteTargetController : ControllerBase
    {
        private readonly IWasteTargetRepository _wasteTargetRepository;

        public WasteTargetController(IWasteTargetRepository wasteTargetRepository)
        {
            _wasteTargetRepository = wasteTargetRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<WasteTargetDataModel>>>> GetPaginated(
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _wasteTargetRepository.Paginate(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<WasteTargetDataModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<WasteTargetDataModel>>($"A megadott adatokkal nincs feltöltve Selejt Target Dimenzió az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<WasteTargetDataModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int id)
        {
            var data = await _wasteTargetRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<WasteTargetDataModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<WasteTargetDataModel>(new APIError(nameof(id), $"A {id} azonosítóval nincs feltöltve Selejt Target Dimenzió az adatbázisba")));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Add([FromBody] WasteTargetDataModel model)
        {
            try
            {
                await _wasteTargetRepository.Add(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval már létezik Selejt Target Dimenzió a rendszerben")));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' Target Dimenzióval már van feltöltve Selejt Target Dimenzió az adatbázisba")));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID,
                                                                 [FromBody] WasteTargetDataModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _wasteTargetRepository.Modify(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik Selejt Target Dimenzió a rendszerben")));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' Target Dimenzióval már van feltöltve Selejt Target Dimenzió az adatbázisba")));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID)
        {
            try
            {
                await _wasteTargetRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik Selejt Target Dimenzió a rendszerben")));
            }
        }
    }
}
