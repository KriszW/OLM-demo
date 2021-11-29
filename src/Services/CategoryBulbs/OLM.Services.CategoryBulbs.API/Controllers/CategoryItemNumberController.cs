using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.CategoryBulbs.API.Models;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Exceptions.DataManagerExceptions;
using OLM.Shared.Extensions.Pagination;

namespace OLM.Services.CategoryBulbs.API.Controllers
{
    [Route("api/category/itemnumber")]
    [ApiController]
    public class CategoryItemNumberController : ControllerBase
    {
        private IItemNumberCategoryRepository _itemNumberCategoryRepository;

        public CategoryItemNumberController(IItemNumberCategoryRepository itemNumberCategoryRepository)
        {
            _itemNumberCategoryRepository = itemNumberCategoryRepository;
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<APIResponse<Paginated<ItemnumberCategoryModel>>>> GetPaginated(
            [FromQuery]string categoryQuery = "",
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _itemNumberCategoryRepository.Search(categoryQuery, skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<ItemnumberCategoryModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<ItemnumberCategoryModel>>($"A megadott adatokkal nincs feltöltve cikk kategória az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<APIResponse<Paginated<ItemnumberCategoryModel>>>> GetPaginated(
            [FromQuery][CustomizeValidator(RuleSet = "PageIndex")] int pageIndex = 0,
            [FromQuery][CustomizeValidator(RuleSet = "PageSize")] int pageSize = 30)
        {
            var skip = pageIndex * pageSize;

            var data = await _itemNumberCategoryRepository.GetPaginated(skip, pageSize);

            if (data != default && data.Data.Any() == true)
            {
                return Ok(new APIResponse<Paginated<ItemnumberCategoryModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<Paginated<ItemnumberCategoryModel>>($"A megadott adatokkal nincs feltöltve cikk kategória az adatbázisba"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<APIResponse<ItemnumberCategoryModel>>> GetByID([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int id)
        {
            var data = await _itemNumberCategoryRepository.GetByID(id);

            if (data != default)
            {
                return Ok(new APIResponse<ItemnumberCategoryModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<ItemnumberCategoryModel>($"A {id} azonosítóval nincs feltöltve cikk kategória az adatbázisba"));
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<EmptyAPIResponse>> Upload([FromBody] ItemnumberCategoryModel model)
        {
            try
            {
                await _itemNumberCategoryRepository.Upload(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (PrimaryKeyAlreadyExistsException<int> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval már létezik cikk kategória a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} cikkszámmal már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpPut]
        [Route("modify/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Modify([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int bundleID, [FromBody] ItemnumberCategoryModel model)
        {
            if (bundleID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _itemNumberCategoryRepository.Update(bundleID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik cikk kategória a rendszerben"));
            }
            catch (UniqueDataAlreadyExistsException<string> ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ColumnValue} cikkszámmal már van feltöltve adat az adatbázisba"));
            }
        }

        [HttpDelete]
        [Route("delete/{bundleID}")]
        public async Task<ActionResult<EmptyAPIResponse>> Delete([FromRoute][CustomizeValidator(RuleSet = "ItemnumberCategorysID")] int bundleID)
        {
            try
            {
                await _itemNumberCategoryRepository.Delete(bundleID);

                return Ok(new EmptyAPIResponse());
            }
            catch (NotFoundByValueException<int> ex)
            {
                return NotFound(new EmptyAPIResponse($"A {ex.ColumnValue} azonosítóval nem létezik cikk kategória a rendszerben"));
            }
        }
    }
}