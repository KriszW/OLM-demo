using OLM.Services.Bundles.API.Models;
using OLM.Shared.Models.Bundles.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Extensions
{
    public static class BundleExtensions
    {
        public static BundleAPIResponseViewModel ConvertToViewModel(this BundleModel model) =>
            new BundleAPIResponseViewModel(model.BundleID,
                                           model.Input,
                                           model.Produced,
                                           model.FS,
                                           model.CalculateWastePercentage(),
                                           model.Dimension,
                                           model.Quality,
                                           model.VendorName,
                                           model.SawmillName,
                                           model.MachineName,
                                           model.FinishedDate);
    }
}
