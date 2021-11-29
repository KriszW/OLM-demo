using OLM.Services.SharedBases.APIErrors;
using OLM.Shared.Models.CategoryBulbs.APIResponses;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.CategoryBulbs
{
    public interface IValidateOneBundleService
    {
        public Task<OneOf<IEnumerable<ValidationResult>, APIError>> ValidateBundle(string bundleID);
    }
}
