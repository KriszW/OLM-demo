using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.BundlePrices.Actions
{
    public class UploadBundlePriceFileFinishedAction
    {
        public UploadBundlePriceFileFinishedAction(string uploadMessage)
        {
            UploadMessage = uploadMessage;
        }

        public string UploadMessage { get; set; }
    }
}
