using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLM.Shared.Models.CategoryBulbs.APIResponses;
using OLM.Services.SharedBases.Responses;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Abstractions;

namespace OLM.Services.CategoryBulbs.API.Controllers
{
    [Route("api/validation")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        private const int HighestCategoryNumber = 4;
        private IBundleItemnumberRepository _bundleItemnumberRepository;

        public ValidatorController(IBundleItemnumberRepository bundleItemnumberRepository)
        {
            _bundleItemnumberRepository = bundleItemnumberRepository;
        }

        [HttpGet]
        [Route("bundle/{bundleID}")]
        public async Task<ActionResult<APIResponse<IEnumerable<ValidationResult>>>> ValidateBundle(string bundleID)
        {
            var output = new List<ValidationResult>();

            for (int i = 1; i <= HighestCategoryNumber; i++)
            {
                var validation = await _bundleItemnumberRepository.HasCategory(bundleID, i.ToString());

                if (validation.Message.Contains("nem létezik"))
                {
                    return NotFound(new APIResponse<IEnumerable<ValidationResult>>($"A {bundleID} rakat nem létezik az adatbázisban"));
                }

                output.Add(new ValidationResult()
                {
                    Description = validation.Message,
                    Name = "IsBundleHasCategory",
                    ValidationSucceded = validation.ValidationSucceded
                });
            }

            return Ok(new APIResponse<IEnumerable<ValidationResult>>(output));
        }
    }
}