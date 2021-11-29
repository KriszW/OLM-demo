using OLM.Shared.Models.Bundles.APIResponses.TCOBundle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Bundles.API.Services.Services.Abstractions
{
    public interface ITCOBundleFileWriterService
    {
        Task<Stream> WriteToFile(IEnumerable<TCOBundleAPIResponseViewModel> data);
    }
}
