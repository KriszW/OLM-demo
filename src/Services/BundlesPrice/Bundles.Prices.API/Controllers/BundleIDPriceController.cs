using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Services.Bundles.Prices.API.Services.Repositories.Abstractions;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;

namespace OLM.Services.Bundles.Prices.API.Controllers
{
    [ApiController]
    public class BundleIDPriceController : ControllerBase
    {
        private readonly IRawBundleIDPriceRepository _rawBundleIDPriceRepository;

        public BundleIDPriceController(IRawBundleIDPriceRepository rawBundleIDPriceRepository)
        {
            _rawBundleIDPriceRepository = rawBundleIDPriceRepository;
        }

        [HttpGet]
        [Route("api/bundle/{bundleID}/prices")]
        public async Task<ActionResult<APIResponse<BundlePriceWithBundleIDsViewModel>>> GetForOne([FromRoute] string bundleID)
        {
            var data = await _rawBundleIDPriceRepository.GetByBundleID(bundleID);

            if (data != default)
            {
                return Ok(new APIResponse<BundlePriceWithBundleIDsViewModel>(data));
            }
            else
            {
                return NotFound(new APIResponse<BundlePriceWithBundleIDsViewModel>($"A '{bundleID}' rakat azonosító nincs feltöltve az adatbázisba, ezért nem elérhető a rakat árja"));
            }
        }

        [HttpGet]
        [Route("/api/bundles/prices")]
        public async Task<ActionResult<APIResponse<IEnumerable<BundlePriceWithBundleIDsViewModel>>>> GetForMany([FromBody] IEnumerable<string> bundleIDs)
        {
            var data = await _rawBundleIDPriceRepository.GetByBundleIDs(bundleIDs);

            if (data != default && data.Any())
            {
                return Ok(new APIResponse<IEnumerable<BundlePriceWithBundleIDsViewModel>>(data));
            }
            else
            {
                return NotFound(new APIResponse<IEnumerable<BundlePriceWithBundleIDsViewModel>>("A megadatt rakat azonosítók nem mindegyike van feltölve az adatbázisba, ezért nem kérhető le hozzájuk a rakatár"));
            }
        }
    }
}
