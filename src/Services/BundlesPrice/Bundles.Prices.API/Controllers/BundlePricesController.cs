using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Services.SharedBases.Requests;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;

namespace OLM.Services.Bundles.Prices.API.Controllers
{
    [ApiController]
    public class BundlePricesController : ControllerBase
    {
        private readonly IPricesRepository _pricesRepository;

        public BundlePricesController(IPricesRepository pricesRepository)
        {
            _pricesRepository = pricesRepository;
        }

        [Route("api/prices/itemnumber/{itemnumber}/vendor/{vendorid}")]
        [HttpGet]
        public async Task<ActionResult<APIResponse<BundlePriceViewModel>>> GetForOne([FromRoute] string itemnumber, [FromRoute] string vendorID)
        {
            var data = await _pricesRepository.GetByItemNumber(new() { RawMaterialItemNumber = itemnumber, VendorID = vendorID });

            if (data != default)
            {
                return Ok(new APIResponse<BundlePriceViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<BundlePriceViewModel>($"A '{vendorID}' Vendorhoz nincs feltöltve rakat ár a '{itemnumber}' cikkszámmal az adatbázisba"));
            }
        }

        [Route("api/prices/itemnumbers/vendors")]
        [HttpGet]
        public async Task<ActionResult<APIResponse<IEnumerable<BundlePriceViewModel>>>> GetForMany([FromBody] IEnumerable<BundlePriceFromItemNumberViewModel> model)
        {
            var data = await _pricesRepository.GetByItemNumbers(model);

            if (data != default && data.Any())
            {
                return Ok(new APIResponse<IEnumerable<BundlePriceViewModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<BundlePriceViewModel>>("A megadatt adatokhoz nem található rakatár az adatbázisba"));
            }
        }
    }
}
