using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Models;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Services.SharedBases.APIErrors;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;

namespace OLM.Services.Bundles.Prices.API.Controllers
{
    [Route("api/bundle/prices/manager")]
    [Authorize]
    [ApiController]
    public class BundlePriceManagerController : ControllerBase
    {
        private readonly IBundlePriceManagerRepository _bundlePriceManagerRepository;

        public BundlePriceManagerController(IBundlePriceManagerRepository bundlePriceManagerRepository)
        {
            _bundlePriceManagerRepository = bundlePriceManagerRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<BundlePriceModel>>>> GetPaginated(
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _bundlePriceManagerRepository.GetPagineted(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<BundlePriceModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<BundlePriceModel>>($"A megadott adatokkal nincs feltöltve ár az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<BundlePriceModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int id)
        {
            var data = await _bundlePriceManagerRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<BundlePriceModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<BundlePriceModel>(new APIError(nameof(id), $"A {id} azonosítóval nincs feltöltve ár az adatbázisba")));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Upload([FromBody] BundlePriceModel model)
        {
            try
            {
                await _bundlePriceManagerRepository.Upload(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval már létezik rakat ár a rendszerben")));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' Vendorhoz már van feltöltve rakat ár a '{model.RawMaterialItemNumber}' cikkszámmal az adatbázisba")));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID,
                                                                 [FromBody] BundlePriceModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _bundlePriceManagerRepository.Modify(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik rakat ár a rendszerben")));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A '{ex.ColumnValue}' Vendorhoz már van feltöltve rakat ár a '{model.RawMaterialItemNumber}' cikkszámmal az adatbázisba")));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "BundlePriceID")] int bundleID)
        {
            try
            {
                await _bundlePriceManagerRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse(new APIError(ex.ColumnName, $"A {ex.ColumnValue} azonosítóval nem létezik rakat ár a rendszerben")));
            }
        }
    }
}
