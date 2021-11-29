using Microsoft.AspNetCore.Http;
using OLM.Services.SharedBases.Responses;
using OLM.Shared.Models.Bundle.Prices.APIResponses;
using OLM.Shared.Models.MoneyExchangeRate.SharedAPIModels.Controllers.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.ApiGateWays.Web.Gtw.SPA.Services.Services.Abstractions.DailyReport
{
    public interface IBundlePriceFileUploadService
    {
        Task<EmptyAPIResponse> Upload(IFormFile file, BundlePriceFileUploadViewModel model);
    }
}
