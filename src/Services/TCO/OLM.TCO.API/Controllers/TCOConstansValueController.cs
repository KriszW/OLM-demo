using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.SharedBases.Responses;
using OLM.Services.TCO.API.Models;
using OLM.Services.TCO.API.Services.Repositories.Abstractions;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;

namespace OLM.Services.TCO.API.Controllers
{
    [Route("api/tco/settings/value")]
    [ApiController]
    [Authorize]
    public class TCOConstansValueController : ControllerBase
    {
        private ITCOSettingsRepository _tcoSettingsRepository;

        public TCOConstansValueController(ITCOSettingsRepository tcoSettingsRepository)
        {
            _tcoSettingsRepository = tcoSettingsRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<TCOValueSettingsModel>>>> GetPaginated(
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _tcoSettingsRepository.GetPaginated(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<TCOValueSettingsModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<TCOValueSettingsModel>>($"A megadott adatokkal nincs feltöltve TCO konstansadatok az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<TCOValueSettingsModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "TCOSettingsID")] int id)
        {
            var data = await _tcoSettingsRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<TCOValueSettingsModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<TCOValueSettingsModel>($"A {id} azonosítóval nincs feltöltve TCO konstansadatok az adatbázisba"));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Upload([FromBody] TCOValueSettingsModel model)
        {
            try
            {
                await _tcoSettingsRepository.Add(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval már létezik TCO konstansadatok a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} cikkszámmal már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "TCOSettingsID")] int bundleID,
                                                                 [FromBody] TCOValueSettingsModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _tcoSettingsRepository.Modify(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik TCO konstansadatok a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} cikkszámmal már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "TCOSettingsID")] int bundleID)
        {
            try
            {
                await _tcoSettingsRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik TCO konstansadatok a rendszerben"));
            }
        }
    }
}